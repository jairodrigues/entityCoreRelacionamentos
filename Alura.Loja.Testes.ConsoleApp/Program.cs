using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var contexto = new LojaContext())
            {
                var cliente = contexto
                                .Clientes
                                .Include(c => c.EnderecoDeEntrega)
                                .FirstOrDefault();

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoDeEntrega.Logradouro}");
                
                var produto = contexto
                                .Produtos                               
                                .Where(p => p.Id == 1014)
                                .FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco < 10)
                    .Load();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine($"Preco : {item.Preco}, Produto {item.Produto}");
                }
            }
        }

        private static void ExibeMuitosParaMuitos()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = contexto
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault();

                Console.WriteLine(promocao);
             
               foreach (var item in promocao.Produtos)
               {
                  Console.WriteLine(item.Produto.Nome);
               }
            }
        }

        private static void SelectSimples()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima total janeiro 2";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataTermino = new DateTime(2017, 1, 31);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "bebidas").ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                contexto.SaveChanges();
            }
        }

        private static void umParaUm()
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulaninho de Tal";
            fulano.EnderecoDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos testes",
                Complemento = "sobrado",
                Bairro = "Analia",
                Cidade = "Cidade"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        private static void muitosParaMuitos()
        {
            
            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                contexto.Promocoes.Add(promocaoDePascoa);
                //var promocao = contexto.Promocoes.Find(2);
                //contexto.Promocoes.Remove(promocao);
                contexto.SaveChanges();
            }
        }

        private static void umParaMuitos()
        {
            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                contexto.Compras.Add(compra);
                contexto.SaveChanges();
            }
        }
    }
}
