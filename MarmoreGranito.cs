using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaMarmoreGranito
{
    class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();
        static List<Bloco> blocos = new List<Bloco>();
        static List<Chapa> chapas = new List<Chapa>();

        static void Main(string[] args)
        {
            // Usuário administrador padrão
            usuarios.Add(new Usuario { Nome = "Administrador", Login = "admin", Senha = "1234" });

            if (FazerLogin())
            {
                Menu();
            }
            else
            {
                Console.WriteLine("Acesso negado. Encerrando o sistema.");
            }
        }

        // =================== LOGIN ===================
        static bool FazerLogin()
        {
            Console.WriteLine("====== LOGIN ======");
            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            var usuario = usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);
            if (usuario != null)
            {
                Console.WriteLine($"Bem-vindo, {usuario.Nome}!\n");
                return true;
            }

            Console.WriteLine("Login ou senha incorretos.\n");
            return false;
        }

        // =================== MENU ===================
        static void Menu()
        {
            int opcao = -1;
            do
            {
                Console.WriteLine("===== MENU =====");
                Console.WriteLine("1 - Cadastro de Usuário");
                Console.WriteLine("2 - Cadastro de Bloco");
                Console.WriteLine("3 - Cadastro de Chapa");
                Console.WriteLine("4 - Processo de Serragem");
                Console.WriteLine("5 - Listar Blocos");
                Console.WriteLine("6 - Listar Chapas");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine();
                    switch (opcao)
                    {
                        case 1: CadastrarUsuario(); break;
                        case 2: CadastrarBloco(); break;
                        case 3: CadastrarChapa(); break;
                        case 4: ProcessoSerragem(); break;
                        case 5: ListarBlocos(); break;
                        case 6: ListarChapas(); break;
                        case 0: Console.WriteLine("Saindo..."); break;
                        default: Console.WriteLine("Opção inválida.\n"); break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida.\n");
                }
            } while (opcao != 0);
        }

        // =================== CADASTRO DE USUÁRIO ===================
        static void CadastrarUsuario()
        {
            Console.WriteLine("=== Cadastro de Usuário ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            usuarios.Add(new Usuario { Nome = nome, Login = login, Senha = senha });
            Console.WriteLine("Usuário cadastrado com sucesso!\n");
        }

        // =================== CADASTRO DE BLOCO ===================
        static void CadastrarBloco()
        {
            Console.WriteLine("=== Cadastro de Bloco ===");
            Console.Write("Código do bloco: ");
            string codigo = Console.ReadLine();

            Console.Write("Pedreira de origem: ");
            string pedreira = Console.ReadLine();

            Console.Write("Metragem em m³: ");
            double metragem = double.Parse(Console.ReadLine());

            Console.Write("Tipo de material: ");
            string tipo = Console.ReadLine();

            Console.Write("Valor de compra: ");
            double valorCompra = double.Parse(Console.ReadLine());

            Console.Write("Número da nota fiscal: ");
            string notaFiscal = Console.ReadLine();

            blocos.Add(new Bloco
            {
                Codigo = codigo,
                PedreiraOrigem = pedreira,
                Metragem = metragem,
                TipoMaterial = tipo,
                ValorCompra = valorCompra,
                NotaFiscal = notaFiscal
            });

            Console.WriteLine("Bloco cadastrado com sucesso!\n");
        }

        // =================== CADASTRO DE CHAPA ===================
        static void CadastrarChapa()
        {
            Console.WriteLine("=== Cadastro de Chapa ===");
            Console.Write("Código do bloco de origem: ");
            string codigoBloco = Console.ReadLine();

            var bloco = blocos.FirstOrDefault(b => b.Codigo == codigoBloco);

            if (bloco == null)
            {
                Console.WriteLine("Bloco não encontrado.\n");
                return;
            }

            Console.Write("Tipo de material: ");
            string tipo = Console.ReadLine();

            Console.Write("Largura (m): ");
            double largura = double.Parse(Console.ReadLine());

            Console.Write("Altura (m): ");
            double altura = double.Parse(Console.ReadLine());

            Console.Write("Espessura (m): ");
            double espessura = double.Parse(Console.ReadLine());

            Console.Write("Valor: ");
            double valor = double.Parse(Console.ReadLine());

            chapas.Add(new Chapa
            {
                BlocoOrigem = bloco,
                TipoMaterial = tipo,
                Largura = largura,
                Altura = altura,
                Espessura = espessura,
                Valor = valor
            });

            Console.WriteLine("Chapa cadastrada com sucesso!\n");
        }

        // =================== PROCESSO DE SERRAGEM ===================
        static void ProcessoSerragem()
        {
            Console.WriteLine("=== Processo de Serragem ===");
            Console.Write("Código do bloco: ");
            string codigoBloco = Console.ReadLine();

            var bloco = blocos.FirstOrDefault(b => b.Codigo == codigoBloco);

            if (bloco == null)
            {
                Console.WriteLine("Bloco não encontrado.\n");
                return;
            }

            Console.Write("Quantidade de chapas a gerar: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.Write("Largura das chapas (m): ");
            double largura = double.Parse(Console.ReadLine());

            Console.Write("Altura das chapas (m): ");
            double altura = double.Parse(Console.ReadLine());

            Console.Write("Espessura das chapas (m): ");
            double espessura = double.Parse(Console.ReadLine());

            Console.Write("Valor por chapa: ");
            double valor = double.Parse(Console.ReadLine());

            for (int i = 0; i < quantidade; i++)
            {
                chapas.Add(new Chapa
                {
                    BlocoOrigem = bloco,
                    TipoMaterial = bloco.TipoMaterial,
                    Largura = largura,
                    Altura = altura,
                    Espessura = espessura,
                    Valor = valor
                });
            }

            Console.WriteLine($"{quantidade} chapas foram geradas a partir do bloco {codigoBloco}.\n");
        }

        // =================== LISTAGEM DE BLOCOS ===================
        static void ListarBlocos()
        {
            Console.WriteLine("=== Lista de Blocos ===");
            if (blocos.Count == 0)
            {
                Console.WriteLine("Nenhum bloco cadastrado.\n");
                return;
            }

            foreach (var bloco in blocos)
            {
                Console.WriteLine($"Código: {bloco.Codigo}, Pedreira: {bloco.PedreiraOrigem}, Metragem: {bloco.Metragem} m³, Tipo: {bloco.TipoMaterial}, Valor: {bloco.ValorCompra}, NF: {bloco.NotaFiscal}");
            }
            Console.WriteLine();
        }

        // =================== LISTAGEM DE CHAPAS ===================
        static void ListarChapas()
        {
            Console.WriteLine("=== Lista de Chapas ===");
            if (chapas.Count == 0)
            {
                Console.WriteLine("Nenhuma chapa cadastrada.\n");
                return;
            }

            foreach (var chapa in chapas)
            {
                Console.WriteLine($"Bloco Origem: {chapa.BlocoOrigem.Codigo}, Tipo: {chapa.TipoMaterial}, Medidas: {chapa.Largura}m x {chapa.Altura}m x {chapa.Espessura}m, Valor: {chapa.Valor}");
            }
            Console.WriteLine();
        }
    }

    // =================== CLASSES ===================
    class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    class Bloco
    {
        public string Codigo { get; set; }
        public string PedreiraOrigem { get; set; }
        public double Metragem { get; set; }
        public string TipoMaterial { get; set; }
        public double ValorCompra { get; set; }
        public string NotaFiscal { get; set; }
    }

    class Chapa
    {
        public Bloco BlocoOrigem { get; set; }
        public string TipoMaterial { get; set; }
        public double Largura { get; set; }
        public double Altura { get; set; }
        public double Espessura { get; set; }
        public double Valor { get; set; }
    }
}
