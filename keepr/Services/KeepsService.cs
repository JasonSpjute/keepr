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
        private readonly VaultsRepository _vr;
        public KeepsService(KeepsRepository kr, VaultsRepository vr)
        {
            _kr = kr;
            _vr = vr;
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
            data.Views++;
            _kr.Edit(data);
            return data;
        }

        internal Keep Create(Keep newKeep)
        {
            newKeep.Id = _kr.Create(newKeep);
            return newKeep;
        }

        internal Keep Edit(Keep editData, string userId)
        {
            Keep original = _kr.Get(editData.Id);
            if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Keep You did not Create"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.Keeps = original.Keeps;
            editData.Views = original.Views;
            editData.Shares = original.Shares;
            editData.CreatorId = original.CreatorId;
            editData.Img = original.Img;
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

        internal IEnumerable<Keep> GetByCreatorId(string id)
        {
            return _kr.GetByCreatorId(id);
        }

        internal IEnumerable<Keep> GetKeepsByVaultId(int id, string userId)
        {
            Vault vault = _vr.Get(id);
            if (vault.CreatorId != userId && vault.IsPrivate == true)
            {
                throw new Exception("This vault is Private");
            }
            return _kr.GetByVaultId(id);

        }
    }
}