using System;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Http;
using CodeWorks.Auth0Provider;
using System.Collections.Generic;
using System.Linq;

namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vr;
        public VaultsService(VaultsRepository vr)
        {
            _vr = vr;
        }
        internal Vault Get(int id, string userId)
        {
            Vault data = _vr.Get(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            else if (data.CreatorId == userId | data.IsPrivate == false)
            {
                return data;
            }
            else
            {
                throw new Exception("This vault is private");
            }
        }

        internal Vault Create(Vault newVault)
        {
            newVault.Id = _vr.Create(newVault);
            return newVault;
        }

        internal Vault Edit(Vault editData, string userId)
        {
            Vault original = Get(editData.Id, userId);
            if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Vault You did not Create"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            return _vr.Edit(editData);
        }


        internal String Delete(int id, string userId)
        {
            Vault original = _vr.Get(id);
            if (original == null) { throw new Exception("Bad ID"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Vault You did not Create"); }
            _vr.Remove(id);
            return "Successfully Deleted";
        }

        internal IEnumerable<Vault> GetByCreatorId(string id, string userId)
        {
            if (id == userId)
            {
                return _vr.GetByCreatorId(id).ToList();
            }
            else
            {
                return _vr.GetByCreatorId(id).ToList().FindAll(r => r.IsPrivate == false);
            }
        }
    }
}