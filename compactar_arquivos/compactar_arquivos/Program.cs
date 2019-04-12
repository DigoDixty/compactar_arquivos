using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace compactar_arquivos
{
    class Program
    {

        static void Main(string[] args)
        {
            //Parametros
            string v_diretorio = Properties.Settings.Default.diretorio_orig;
            string v_diretorio_tmp = v_diretorio + "\\temp";
            string v_extensao = Properties.Settings.Default.extensao_orig;
            string file_log = v_diretorio + "\\log_" + (DateTime.Now.ToString("yyyyMMdd_hhmmss")) + ".txt";

            escreve_log(file_log, "Inicio de console para compactação de arquivos.");
            escreve_log(file_log, "");
            escreve_log(file_log, "Diretório a procurar: " + v_diretorio);
            escreve_log(file_log, "Extensoes a procurar: " + v_extensao);
            escreve_log(file_log, "");

            string[] ListDiretorios = Directory.GetDirectories(v_diretorio);
            for (int cont_dir = 0; cont_dir < ListDiretorios.Length; cont_dir++)
            {
                Console.WriteLine("");
                Console.WriteLine("Diretorio encontrado: " + ListDiretorios[cont_dir]);
                escreve_log(file_log, "");
                escreve_log(file_log, "Diretorio encontrado: " + ListDiretorios[cont_dir]);
                string nome_dir = ListDiretorios[cont_dir].Replace(v_diretorio, "");
                string v_diretorio_sub = v_diretorio + nome_dir;

                string[] ListArquivos = Directory.GetFiles(v_diretorio_sub, v_extensao);

                for (int conf_arq = 0; conf_arq < ListArquivos.Length; conf_arq++)
                {

                    Console.WriteLine("Arquivo encontrado: " + ListArquivos[conf_arq]);
                    escreve_log(file_log, "Arquivo encontrado: " + ListArquivos[conf_arq]);

                    string NomeArquivo = ListArquivos[conf_arq].Replace(v_diretorio_sub, "");
                    string ext_file = Path.GetExtension(v_diretorio_sub + NomeArquivo);
                    string NomeArquivo_zip = NomeArquivo.Replace(ext_file, ".zip");

                    if (!Directory.Exists(v_diretorio_tmp))
                    {
                        Directory.CreateDirectory(v_diretorio_tmp);
                    }

                    File.Move(v_diretorio_sub + NomeArquivo, v_diretorio_tmp + NomeArquivo);
                    //escreve_log(file_log, "Arquivo movido para: " + v_diretorio_tmp + NomeArquivo);

                    ZipFile.CreateFromDirectory(v_diretorio_tmp, v_diretorio_sub + NomeArquivo_zip);
                    Console.WriteLine("Arquivo compactado: " + v_diretorio_sub + NomeArquivo_zip);
                    escreve_log(file_log, "Arquivo compactado: " + v_diretorio_sub + NomeArquivo_zip);
                    escreve_log(file_log, "");
                    Directory.Delete(v_diretorio_tmp, true);

                }

            }

            escreve_log(file_log, "Fim da execucao!");

        }

        static void escreve_log(string file_log, string texto)
        {
            using (StreamWriter w = File.AppendText(file_log))
            {
                w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " : " + texto);
            }
        }

    }

}
