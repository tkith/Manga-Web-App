using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MangaAPI.Models;

namespace MangaAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MangaAPIContext context)
        {
            context.Database.EnsureCreated();
            var users = new User[]
            {
                new User(name:"admin", email: "admin@mangas.com", password: "admin"),
                new User(name:"admin2", email: "admin2@mangas.com", password: "admin2"),
                new User(name:"admin3", email: "admin3@mangas.com", password: "admin3"),
                new User(name:"admim4", email: "admin4@mangas.com", password: "admin4"),
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var newMangas = new Manga[]
            {
                new Manga(
                    title: "The Breaker", 
                    description:"The Breaker raconte l'histoire d'un lycéen se nommant Shi-Woon et qui se fait tabasser, racketter par les membres d'une bande se trouvant dans la même classe que lui. Un jour, après s'être fait tabasser pour ne pas avoir rapporté l'argent demandé, un jeune homme pour le moins étrange, se prénommant Chun Woo, apparaît et se moque de la faiblesse de Shi-Woon. Mais au plus grand désarrois de ce-dernier, Chun Woo s'avère être le professeur remplaçant d'anglais. Sauf que ce remplaçant n'est pas un professeur comme les autres : en effet, c'est un maître en arts martiaux. Shi-Woon le découvre et lui demande de les lui apprendre pour protéger les gens auxquels il tient (dont celle qu'il aime... ), de la bande qui le martyrise.", 
                    price: 7.99, 
                    genre: "Action", 
                    year: 2007
                ),
                new Manga(
                    title: "My Hero Academia",
                    description:"Dans un monde où 80 % de la population possède un super‑pouvoir appelé alter, les héros font partie de la vie quotidienne. Et les super‑vilains aussi ! Face à eux se dresse l'invincible All Might, le plus puissant des héros ! Le jeune Izuku Midoriya en est un fan absolu. Il n'a qu'un rêve : entrer à la Hero Academia pour suivre les traces de son idole. Le problème, c'est qu'il fait partie des 20 % qui n'ont aucun pouvoir... Son destin est bouleversé le jour où sa route croise celle d'All Might en personne ! Ce dernier lui offre une chance inespérée de voir son rêve se réaliser. Pour Izuku, le parcours du combattant ne fait que commencer !",
                    price: 6.60,
                    genre: "Super-héros",
                    year: 2014
                )
            };

            foreach (Manga m in newMangas)
            {
                   context.Mangas.Add(m);
            }
            context.SaveChanges();

            var comments = new Comment[]
            {
                new Comment(mangaId:1, userId: 1, text:"comment 1" ),
                new Comment(mangaId:1, userId: 1, text:"comment 2" ),
                new Comment(mangaId:1, userId: 1, text:"comment 3" ),
             };

            foreach (Comment c in comments)
            {
                    context.Comments.Add(c);
            }
            context.SaveChanges();

            var ratings = new Rating[]
            {
                new Rating(value:5, mangaId: 1, userId: 1),
                new Rating(value:3, mangaId: 2, userId: 1),
                new Rating(value:4, mangaId: 2, userId: 2),
                new Rating(value:5, mangaId: 2, userId: 3)
            };

            foreach (Rating r in ratings)
            {
                context.Ratings.Add(r);
            }
            context.SaveChanges();
        }
    }
}
