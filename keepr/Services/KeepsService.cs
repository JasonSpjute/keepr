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

        internal Keep Get(int id)
        {
            var data = _kr.Get(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Keep Create(Keep newKeep)
        {
            newKeep.Id = _kr.Create(newKeep);
            return newKeep;
        }

        internal Keep Edit(Keep editData, string userId)
        {
            Keep original = Get(editData.Id);
            if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Keep You did not Create"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            return _kr.Edit(editData);
        }

        internal String Delete(int id, string userId)
        {
            Keep original = _kr.Get(id);
            if (original == null) { throw new Exception("Bad ID"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Keep You did not Create"); }
            _kr.Remove(id);
            return "Successfully Deleted";
        }
    }
}