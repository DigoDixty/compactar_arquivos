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
            string file_log = "d:\\temp\\log_" + (DateTime.Now.ToString("yyyyMMdd_hhmmss")) + ".log";

            escreve_log(file_log, "Inicio de console para compactação de arquivos.");

            //Parametros
            string v_diretorio = Properties.Settings.Default.diretorio_orig;
            string v_diretorio_tmp = v_diretorio + "\\temp";
            string v_extensao = Properties.Settings.Default.extensao_orig;

            escreve_log(file_log, "");
            escreve_log(file_log, "Diretório a procurar: " + v_diretorio);
            escreve_log(file_log, "Extensoes a procurar: " + v_extensao);
            escreve_log(file_log, "");

            string[] ListArquivos = Directory.GetFiles(v_diretorio, v_extensao);

            for (int i = 0; i < ListArquivos.Length; i++)
            {

                Console.WriteLine("Arquivo encontrado: " + ListArquivos[i]);

                escreve_log(file_log, "Arquivo encontrado: " + ListArquivos[i]);

                string NomeArquivo = ListArquivos[i].Replace(v_diretorio, "");
                string ext_file = Path.GetExtension(v_diretorio + NomeArquivo);
                string NomeArquivo_zip = NomeArquivo.Replace(ext_file, ".zip");

                if (!Directory.Exists(v_diretorio_tmp))
                {
                    Directory.CreateDirectory(v_diretorio_tmp);
                }

                File.Move(v_diretorio + NomeArquivo, v_diretorio_tmp + NomeArquivo);
                escreve_log(file_log, "Arquivo movido para: " + v_diretorio_tmp + NomeArquivo);

                ZipFile.CreateFromDirectory(v_diretorio_tmp, v_diretorio + NomeArquivo_zip);
                Console.WriteLine("Arquivo compactado: " + v_diretorio + NomeArquivo_zip);
                escreve_log(file_log, "Arquivo compactado: " + v_diretorio + NomeArquivo_zip);

                Directory.Delete(v_diretorio_tmp, true);

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
