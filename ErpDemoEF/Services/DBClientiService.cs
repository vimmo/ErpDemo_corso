﻿using ErpDemoEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErpDemoEF.Services
{
    public interface IDBClientiService
    {
        public Clienti CreaCliente(Clienti cliente);
        public bool ModificaCliente(Clienti cliente);
        public Clienti LeggiCliente(int id);
        //public Clienti LeggiCliente(bool bMaxValue);
        public Clienti LeggiCliente(int currId, bool bNext);
        public IEnumerable<Clienti> LeggiListaClienti();
        public bool EliminaCliente(Clienti cliente);
    }
    public class DBClientiService : IDBClientiService
    {
        private readonly ErpDemoContext _context;
        public DBClientiService()
        {
            _context = new ErpDemoContext();
        }
        public Clienti CreaCliente(Clienti cliente)
        {
            _context.Clienti.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }
        public bool ModificaCliente(Clienti cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            int rowAffected = _context.SaveChanges();
            if (rowAffected > 0)
                return true;
            else
                return false;
        }
        public Clienti LeggiCliente(int id)
        {
            return _context.Clienti.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
        }
        public Clienti LeggiCliente(int currId, bool bNext)
        {
                Clienti val = null;
                if (bNext)
                {
                    if(currId != 0)
                        val = _context.Clienti.AsNoTracking().Where(c => c.Id > currId).OrderBy(c => c.Id).FirstOrDefault();
                    if (val == null)
                        val = _context.Clienti.AsNoTracking().Where(c => c.Id == _context.Clienti.Max(c => c.Id)).FirstOrDefault();
                }
                else
                {
                    if (currId != 0)
                        val = _context.Clienti.AsNoTracking().Where(c => c.Id < currId).OrderByDescending(c => c.Id).FirstOrDefault();
                    if (val == null)
                        val = _context.Clienti.AsNoTracking().Where(c => c.Id == _context.Clienti.Min(c => c.Id)).FirstOrDefault();
                }

                return val;
        }
        //public Clienti LeggiCliente(bool bMaxValue)
        //{
        //    int i = 0;
        //    if (bMaxValue)
        //        i = _context.Clienti.Max(c => c.Id);
        //    else
        //        i = _context.Clienti.Min(c => c.Id);

        //    return _context.Clienti.AsNoTracking().Where(c => c.Id == i).FirstOrDefault();
        //}
        public IEnumerable<Clienti> LeggiListaClienti()
        {
            return _context.Clienti.AsNoTracking();
        }
        public bool EliminaCliente(Clienti cliente)
        {
            _context.Clienti.Remove(cliente);
            int rowAffected =_context.SaveChanges();
            return rowAffected > 0 ? true : false;
        }

    }
}
