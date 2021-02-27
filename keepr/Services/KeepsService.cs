using System;
using System.Collections;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _kr;
        public KeepsService(KeepsRepository kr)
        {
            _kr = kr;
        }
        public IEnumerable<Keep> GetAll()
        {
            IEnumerable<Keep> keeps = _kr.GetAll();
            return keeps;
        }

        internal Keep Create(Keep newKeep)
        {
            newKeep.Id = _kr.Create(newKeep);
            return newKeep;
        }
    }
}