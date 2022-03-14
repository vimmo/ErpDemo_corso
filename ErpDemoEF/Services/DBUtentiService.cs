using ErpDemoEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErpDemoEF.Services
{
    public interface IDBUtentiService
    {
        public Utenti Login(Utenti utente);
        public Sessioni SessioneAttiva(string username);
        public void CreaSessione(string username);
        public void EliminaSessione(string username);

    }

    public class DBUtentiService : IDBUtentiService
    {
        private readonly ErpDemoContext _context;
        public DBUtentiService()
        {
            _context = new ErpDemoContext();
        }
        public Utenti Login(Utenti utente)
        {
            return _context.Utenti.AsNoTracking().Where(u => u.username == utente.username && u.password == utente.password).SingleOrDefault();
        }
        public void CreaSessione(string username)
        {
            _context.Sessioni.Add(new Sessioni { username = username, creazione = DateTime.Now });
            _context.SaveChanges();
        }
        public Sessioni SessioneAttiva(string username)
        { 
            return _context.Sessioni.AsNoTracking().Where(u => u.username == username).SingleOrDefault();
        }
        public void EliminaSessione(string username)
        {
            Sessioni se = SessioneAttiva(username);
            if (se != null)
            {
                _context.Sessioni.Remove(se);
                _context.SaveChanges();
            }
        }
    }
}
