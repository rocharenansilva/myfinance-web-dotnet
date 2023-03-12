using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfinance_web_dotnet.Domain.Entities;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace myfinance_web_dotnet.Domain.Services
{
  public class TransacaoService : ITransacaoService
  {
    private readonly MyFinanceDbContext _dbContext;

    public TransacaoService(MyFinanceDbContext dbContext) 
    {
      _dbContext = dbContext;
    }

    public void Excluir(int id)
    {
      var item = _dbContext.Transacao.Where(item => item.Id == id).First();
      _dbContext.Attach(item);
      _dbContext.Remove(item);
      _dbContext.SaveChanges();
    }

    public List<TransacaoModel> ListarRegistros()
    {
      var result = new List<TransacaoModel>();
      var dbSet = _dbContext.Transacao.Include(dbSet => dbSet.PlanoConta);

      foreach(var item in dbSet)
      {
        var itemTransacao = new TransacaoModel()
        {
          Id = item.Id,
          Data = item.Data,
          Historico = item.Historico,
          Valor = item.Valor,
          ItemPlanoConta = new PlanoContaModel()
          {
            Id = item.PlanoConta.Id,
            Descricao = item.PlanoConta.Descricao,
            Tipo = item.PlanoConta.Tipo
          },
          PlanoContaId = item.PlanoContaId
        };

        result.Add(itemTransacao);
      }

      return result;
    }

    public TransacaoModel RetornarRegistro(int id)
    {
      var item = _dbContext.Transacao.Where(item => item.Id == id).First();

      var itemTransacao = new TransacaoModel()
      {
        Id = item.Id,
        Data = item.Data,
        Historico = item.Historico,
        Valor = item.Valor,
        PlanoContaId = item.PlanoContaId
      };

      return itemTransacao;
    }

    public void Salvar(TransacaoModel model)
    {
      var dbSet = _dbContext.Transacao;
      
      var entidade = new Transacao()
      {
        Id = model.Id,
        Data = model.Data,
        Historico = model.Historico,
        Valor = model.Valor,
        PlanoContaId = model.PlanoContaId
      };

      if (entidade.Id == null)
      {
        dbSet.Add(entidade);
      } 
      else 
      {
        dbSet.Attach(entidade);
        _dbContext.Entry(entidade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      }

      _dbContext.SaveChanges();
    }
  }
}