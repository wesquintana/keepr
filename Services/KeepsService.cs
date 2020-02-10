using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    public Keep GetById(int id, string userId)
    {
      Keep k = _repo.GetById(id, userId);
      if (k == null) { throw new Exception("Can't get that keep."); }
      return k;
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }
    public Keep Edit(Keep update)
    {
      int k = _repo.Edit(update);
      if (k == 0) { throw new Exception("You can't do that."); }
      return update;
    }
    public int ViewKeep(int id)
    {
      return _repo.ViewKeep(id);

    }
    public int KeepKeep(int id)
    {
      return _repo.KeepKeep(id);
    }
    public void Delete(int id, string userId)
    {
      int k = _repo.Delete(id, userId);
      if (k == 0) { throw new Exception("You can't do that."); }
    }
  }
}