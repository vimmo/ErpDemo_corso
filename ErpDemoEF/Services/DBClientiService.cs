using ErpDemoEF.Models;
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
        public Clienti LeggiCliente(bool max);
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

            //return rowAffected > 0 ? true : false;
        }
        public Clienti LeggiCliente(int id)
        {
            return _context.Clienti.Find(id);
        }
        public Clienti LeggiCliente(bool bMaxValue)
        {
            //_context.Clienti.Where(cliente => cliente.Citta == "Genova");

            ////int[] test = { 1, 2, 3, 4 };
            ////test.Max(i => i);
            //IEnumerable<Clienti> a = _context.Clienti;
            //List<Clienti> clientiLista = a.ToList();

            //int max = clientiLista.Max(cliente => cliente.Id);

            //int massimo = 0;
            //int lastId = 0;
            //int[] test = { 1, 2, 3, 4 };
            //int mass = test.Max();
            //foreach (int value in test)
            //{
            //    if (value > massimo)
            //        massimo = value;
            //}

            int i = 0;
            if(bMaxValue)
                i = _context.Clienti.Max(c => c.Id);
            else
                i = _context.Clienti.Min(c => c.Id);
            //return _context.Clienti.Find(i);
            return _context.Clienti.Where(c => c.Id == i).FirstOrDefault();
        }
        public IEnumerable<Clienti> LeggiListaClienti()
        {
            return _context.Clienti;
        }
        public bool EliminaCliente(Clienti cliente)
        {
            _context.Clienti.Remove(cliente);
            int rowAffected =_context.SaveChanges();
            return rowAffected > 0 ? true : false;
        }

    }
}
