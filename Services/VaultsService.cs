using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }
    public Vault GetById(int id, string userId)
    {
      Vault v = _repo.GetById(id, userId);
      if (v == null) { throw new Exception("Can't get that vault."); }
      return v;
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
    public Vault Edit(Vault update)
    {
      int v = _repo.Edit(update);
      if (v == 0) { throw new Exception("You can't do that?"); }
      return update;
    }
    public void Delete(int id, string userId)
    {
      int i = _repo.Delete(id, userId);
      if (i == 0) { throw new Exception("You can't do that."); }
    }
  }
}