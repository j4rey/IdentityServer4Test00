using ApiHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHost.Repositories
{
    public interface IDataAccess<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetWebsites();
        TEntity GetWebsite(U id);
        int AddWebsite(TEntity b);
        int UpdateWebsite(U id, TEntity b);
        int DeleteWebsite(U id);
    }

    public class IDataAccess : IDataAccess<GrayScaleWebsite, int>
    {
        ApplicationContext ctx;
        public IDataAccess(ApplicationContext c)
        {
            ctx = c;
        }

        public int AddWebsite(GrayScaleWebsite b)
        {
            ctx.GrayScaleWebsites.Add(b);
            int res = ctx.SaveChanges();
            return res;
        }

        public int DeleteWebsite(int id)
        {
            int res = 0;
            var book = ctx.GrayScaleWebsites.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                ctx.GrayScaleWebsites.Remove(book);
                res = ctx.SaveChanges();
            }
            return res;
        }

        public GrayScaleWebsite GetWebsite(int id)
        {
            var book = ctx.GrayScaleWebsites.FirstOrDefault(b => b.Id == id);
            return book;
        }

        public IEnumerable<GrayScaleWebsite> GetWebsites()
        {
            var books = ctx.GrayScaleWebsites.ToList();
            return books;
        }

        public int UpdateWebsite(int id, GrayScaleWebsite b)
        {
            int res = 0;
            var book = ctx.GrayScaleWebsites.Find(id);
            if (book != null)
            {
                book.Title = b.Title;
                book.Name = b.Name;
                book.Description = b.Description;
                res = ctx.SaveChanges();
            }
            return res;
        }
    }
}
