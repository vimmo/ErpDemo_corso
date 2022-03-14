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
            return _context.Utenti.AsNoTracking().Where(u => u.username == utente.username && u.password == utente.password).SingleOrDefault(); ;
        }

    }
}
