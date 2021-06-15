#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ActorDto = Domain.Wol.Actor;
using GuildDto = Domain.Wol.Guild;
using ReportDto = Domain.Wol.Report;
using CombatLogDto = Domain.Wol.CombatLog;
using Guild = Domain.Entities.Guild;
using Report = Domain.Entities.Report;
using SimpleQueryDto = Domain.Wol.SimpleQuery;
using Spell = Domain.Entities.Spell;
using SpellDto = Domain.Wol.Spell;

namespace FileImporter
{
    public class Mapper
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;
        
        private readonly IDictionary<long, Guild> _guilds = new Dictionary<long, Guild>();
        private readonly IDictionary<string, Report> _reports = new Dictionary<string, Report>();
        private readonly IDictionary<int, Spell> _spells = new Dictionary<int, Spell>();

        public Mapper(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public void Map(
            GuildDto guildDto,
            ReportDto reportDto,
            CombatLogDto combatLogDto,
            SimpleQueryDto simpleQueryDto
            )
        {
            Guild guild = GetOrMapDbValue(guildDto.gid, guildDto, _guilds, _mapper, _db.Guilds, _ => _.Id == guildDto.gid);
            Report report = GetOrMapValueToDictionary(reportDto.sid, reportDto, _reports, _mapper, entity =>
            {
                entity.Guild = guild;
            });

            foreach (var dto in combatLogDto.spells)
            {
                GetOrMapDbValue(dto.id, dto, _spells, _mapper, _db.Spells, _ => _.Id == dto.id, entity =>
                {
                    entity.Schools = ProcessSchools(_db.Schools, dto);
                    _db.Entry(entity).State = EntityState.Added;
                });
            }
            
            // todo: actors, flaggedActors, events, lines don't need to be mapped for the import
            
            foreach (var dto in combatLogDto.actors)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.Actors, _mapper, entity =>
                {
                    entity.Report = report;
                });
            }

            foreach (var dto in combatLogDto.flaggedActors)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.FlaggedActors, _mapper, entity =>
                {
                    entity.Report = report;
                    entity.Actor = report.Actors[dto.aid];
                });
            }
            
            foreach (var dto in combatLogDto.events)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.Events, _mapper, entity =>
                {
                    entity.Report = report;

                    entity.Spell = HandleSpell(dto.spell, _spells);
                    entity.ExtraSpell = HandleSpell(dto.extraSpell, _spells);
                });
            }

            foreach (var lineId in simpleQueryDto.lines)
            {
                int[]? dto = combatLogDto.entries.FirstOrDefault(_ => _[0] == lineId);

                if (dto is null)
                {
                    throw new ApplicationException("line doesn't exists");
                }
                
                var line = new Line()
                {
                    Report = report,
                    Id = dto[0],
                    Timestamp = dto[1],
                    Event = report.Events[dto[4]],
                };

                if (dto[2] >= 0)
                {
                    line.SourceFlaggedActor = report.FlaggedActors[dto[2]];
                }
                if (dto[3] >= 0)
                {
                    line.TargetFlaggedActor = report.FlaggedActors[dto[3]];
                }
                
                report.Lines.Add(line);
            }
            
            return;
        }

        public IEnumerable<Report> GetData()
        {
            return _reports.Values;
        }

        private static IEnumerable<School> ProcessSchools(DbSet<School> schools, SpellDto spellDto)
        {
            var schoolFlags = spellDto.school;
            var queue = new Queue<string>(spellDto.schools);
            var list = new List<School>();

            while (queue.Count > 0)
            {
                var schoolName = queue.Dequeue();
                var school = schools.Local.FirstOrDefault(_ => _.Name == schoolName);

                if (school is null)
                {
                    if (queue.Count > 0)
                    {
                        throw new ApplicationException($"School id can't be determined for {schoolName}");
                    }
                        
                    school = new School() {Name = schoolName, Id = schoolFlags};
                    schools.Add(school);
                }

                schoolFlags -= school.Id;
                list.Add(school);
            }

            return list;
        }

        private static TValue GetOrMapValueToDictionary<TKey, TValue>(TKey key, object dto, IDictionary<TKey, TValue> dictionary, IMapper mapper, Action<TValue>? postMappingAction = default)
        {
            if (!dictionary.TryGetValue(key, out var entity))
            {
                entity = mapper.Map<TValue>(dto);
                postMappingAction?.Invoke(entity);
                
                dictionary.Add(key, entity);
            }

            return entity;
        }   
        private static TValue GetOrMapDbValue<TKey, TValue>(
            TKey key, 
            object dto,
            IDictionary<TKey, TValue> dictionary,
            IMapper mapper,
            DbSet<TValue> db,
            Func<TValue, bool> comparer,
            Action<TValue>? postMappingAction = default
            ) 
            where TValue : class
        {
            if (!dictionary.TryGetValue(key, out var entity))
            {
                entity = db.Local.FirstOrDefault(comparer);
                
                if (entity is null)
                {
                    entity = mapper.Map<TValue>(dto);
                    postMappingAction?.Invoke(entity);
                    
                    db.Add(entity);
                }

                dictionary.Add(key, entity);
            }

            return entity;
        }

        private static Spell? HandleSpell(int? spellDto, IDictionary<int, Spell> spells)
        {
            if (!spellDto.HasValue)
            {
                return default;
            }
                    
            if (!spells.ContainsKey(spellDto.Value))
            {
                throw new ApplicationException("spell does not exists");
            }
            
            return spells[spellDto.Value];
        }
    }
}