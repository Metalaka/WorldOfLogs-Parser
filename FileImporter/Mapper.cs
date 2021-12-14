#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileImporter
{
    using Domain;

    public class Mapper
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;
        private readonly LogHelper _logHelper;

        private readonly IDictionary<long, Guild> _guilds = new Dictionary<long, Guild>();
        private readonly IDictionary<string, Report> _reports = new Dictionary<string, Report>();
        private readonly IDictionary<int, Spell> _spells = new Dictionary<int, Spell>();

        private Report? _lastReport;

        public Mapper(IMapper mapper, DataContext db, LogHelper logHelper)
        {
            _mapper = mapper;
            _db = db;
            _logHelper = logHelper;
        }
        
        public void MapSynchronously(Queue<PageData> queue)
        {
            var i = 0;
            var length = queue.Count;

            while (queue.Count > 0)
            {
                _logHelper.LogProgression(ref i, length, 500);

                try
                {
                    PageData data = queue.Dequeue();

                    Map(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Map(PageData data)
        {
            // todo: pre-optimisation to refactor
            if (data.Guild is not null)
            {
                Guild guild = GetOrMapDbValue(data.Guild.gid, data.Guild, _guilds, _mapper, _db.Guilds, _ => _.Id == data.Guild.gid);
                _lastReport = GetOrMapValueToDictionary(data.Report.sid, data.Report, _reports, _mapper, entity =>
                {
                    entity.Guild = guild;
                    
                    Report? report = _db.Reports.FirstOrDefault(_ => _.Sid == entity.Sid);
                    if (report is not null)
                    {
                        entity.Id = report.Id;
                    }
                    else
                    {
                        throw new ApplicationException();
                    }                    
                });
            }

            if (_lastReport is null)
            {
                throw new ApplicationException("No report found");
            }
            
            Report report = _lastReport;

            foreach (var dto in data.CombatLog.spells)
            {
                GetOrMapDbValue(dto.id, dto, _spells, _mapper, _db.Spells, _ => _.Id == dto.id, entity =>
                {
                    entity.Schools = ProcessSchools(_db.Schools, dto);
                    _db.Entry(entity).State = EntityState.Added;
                });
            }
            
            // todo: actors, flaggedActors, events, lines don't need to be mapped for the import
            
            foreach (var dto in data.CombatLog.actors)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.Actors, _mapper, entity =>
                {
                    entity.Report = report;
                });
            }

            foreach (var dto in data.CombatLog.flaggedActors)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.FlaggedActors, _mapper, entity =>
                {
                    entity.Report = report;
                    entity.Actor = report.Actors[dto.aid];
                });
            }
            
            foreach (var dto in data.CombatLog.events)
            {
                GetOrMapValueToDictionary(dto.id, dto, report.Events, _mapper, entity =>
                {
                    entity.Report = report;

                    entity.Spell = HandleSpell(dto.spell, _spells);
                    entity.ExtraSpell = HandleSpell(dto.extraSpell, _spells);
                });
            }

            foreach (var lineId in data.SimpleQuery.lines)
            {
                int[]? dto = data.CombatLog.entries.FirstOrDefault(_ => _[0] == lineId);

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

        private static IEnumerable<School> ProcessSchools(DbSet<School> schools, Domain.Wol.Spell spellDto)
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