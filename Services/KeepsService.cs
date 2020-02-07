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
      return _repo.GetById(id, userId);
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }
    public Keep Edit(Keep update)
    {
      Keep exists = _repo.GetById(update.Id, update.UserId);
      if (exists == null) { throw new Exception("You can't do that?"); }
      return _repo.Edit(update);
    }
    public void Delete(int id, string userId)
    {
      _repo.Delete(id, userId);
    }
  }
}