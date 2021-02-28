using System;
using keepr.Models;
using keepr.Repositories;
using Party_Planner.Exceptions;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsRepository _vr;
        private readonly KeepsRepository _kr;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vr, KeepsRepository kr)
        {
            _repo = repo;
            _vr = vr;
            _kr = kr;
        }
        internal VaultKeep Create(VaultKeep vk)
        {
            Vault vault = _vr.Get(vk.VaultId);
            Keep keep = _kr.Get(vk.KeepId);
            if (vault == null)
            {
                throw new Exception("Invalid Vault Id");
            }
            if (keep == null)
            {
                throw new Exception("Invalid Keep Id");
            }
            if (vault.CreatorId != vk.CreatorId)
            {
                throw new NotAuthorized("Not the Owner of this Vault");
            }
            return _repo.Create(vk);
        }
        internal void Delete(int id, string userId)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            if (data.CreatorId != userId)
            {
                throw new Exception("This is not yours to delete");
            }
            _repo.Delete(id);
        }
    }
}