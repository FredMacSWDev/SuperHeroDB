﻿using SuperFriendsDB.Data;
using SuperFriendsDB.Models.BiographyModels;
using SuperFriendsDB.Models.BiographyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFriendsDB.Services
{
    public class BiographyService
    {
        private readonly Guid _userId;
        public BiographyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBio(BiographyCreate model)
        {
            var entity =
                new Biography()
                {
                    BioId = model.BioId,
                    CharacterId = model.CharacterId,
                    FullName = model.FullName,
                    AlterEgos = model.AlterEgos,
                    PlaceOfBirth = model.PlaceOfBirth,
                    FirstAppearance = model.FirstAppearance,
                    Publisher = model.Publisher,
                    Alignment = model.Alignment
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bio.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BiographyListItem> GetBio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bio
                        .Select(
                            e =>
                                new BiographyListItem
                                {
                                    BioId = e.BioId,
                                    CharacterId = e.CharacterId,
                                    FullName = e.FullName,
                                    AlterEgos = e.AlterEgos,
                                    PlaceOfBirth = e.PlaceOfBirth,
                                    FirstAppearance = e.FirstAppearance,
                                    Publisher = e.Publisher,
                                    Alignment = e.Alignment
                                });

                return query.ToArray();
            }
        }

        public BiographyDetail GetBioById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bio
                        .Single(e => e.BioId == id);
                return
                    new BiographyDetail
                    {
                        BioId = entity.BioId,
                        CharacterId = entity.CharacterId,
                        FullName = entity.FullName,
                        AlterEgos = entity.AlterEgos,
                        PlaceOfBirth = entity.PlaceOfBirth,
                        FirstAppearance = entity.FirstAppearance,
                        Publisher = entity.Publisher,
                        Alignment = entity.Alignment
                    };

            }
        }

        public bool UpdateBio(BiographyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bio
                        .Single(e => e.BioId == model.BioId);

                entity.FullName = model.FullName;
                entity.AlterEgos = model.AlterEgos;
                entity.PlaceOfBirth = model.PlaceOfBirth;
                entity.FirstAppearance = model.FirstAppearance;
                entity.Publisher = model.Publisher;
                entity.Alignment = model.Alignment;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBio(int bioId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bio
                        .Single(e => e.BioId == bioId);

                ctx.Bio.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
