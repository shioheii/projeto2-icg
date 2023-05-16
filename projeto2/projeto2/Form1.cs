/* Colegio Técnico Antônio Teixeira Fernandes (Univap)
 * Curso Técnico em Informática - Data de Entrega: 24/05/2023
 * Autores do Projeto: Arthur de Nazareth Falcão Braga
 *                     Bruno Shiohei Kinoshita do Nascimento 
 *   
 * Turma: 3H
 * Atividade Proposta em aula
 * Observação: <colocar se houver>
 * 
 * 
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

namespace projeto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool marcarCoordenadas = false;
        bool validarDesenho = false;
        int desenhoSelecionado = 0; // 1 - Reta / 2 - Retangulo / 3 - Circulo / 4 - Elipse / 5 - Trialgulo / 6 - Losango / 7 - Pentagono
        int clicks = 0;
        int[] coordenadas = new int[10];
        int raio, altura, largura; // Caso da elipse e circulo // Dá pra fazer var local 
        int indiceCoordenadas = 0;
        int formatoSelecionado = 1;
        float[] formatoLinha = { 1 };
        int espessuraSelecionada = 1;
        int espessura = 1;
        int corSelecionada = 1;
        Color cor;
        
        Color color(int r, int g, int b, PaintEventArgs e)
        {
            return Color.FromArgb(r, g, b);
        }
        
        Pen criarCaneta(Color cor, float[] fLinha, int espessura, PaintEventArgs e) // Só pra testes manuais
        {
            Pen caneta = new Pen(cor, espessura);
            caneta.DashPattern = fLinha;
            return caneta;
        }

        void ponto(int x, int y, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawLine(caneta, x, y, x + 1, y);
        }

        void linha(int x0, int y0, int x1, int y1, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawLine(caneta, x0, y0, x1, y1);
        }

        void retangulo(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            int[] indices = { 0, 1, 2, 1, 2, 1, 2, 3, 2, 3, 0, 3, 0, 3, 0, 1 };
            for (int i = 0; i < indices.Length; i+=4)
                linha(pontos[indices[i]], pontos[indices[i+1]], pontos[indices[i+2]], pontos[indices[i+3]], caneta, e);
        }

        void circulo(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(caneta, pontos[0], pontos[1], raio, raio);
        }

        void elipse(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(caneta, pontos[0], pontos[1], altura, largura);
        }

        void triangulo(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            int[] indices = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 0, 1 };
            for (int i = 0; i < indices.Length; i+=4)
                linha(pontos[indices[i]], pontos[indices[i+1]], pontos[indices[i+2]], pontos[indices[i+3]], caneta, e);
        }

        void losango(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            int[] indices = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 0, 1 };
            for (int i = 0; i < indices.Length; i+=4)
                linha(pontos[indices[i]], pontos[indices[i+1]], pontos[indices[i+2]], pontos[indices[i+3]], caneta, e);
        }

        void pentagono(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            int[] indices = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 8, 9, 8, 9, 0, 1 };
            for (int i = 0; i < indices.Length; i+=4)
                linha(pontos[indices[i]], pontos[indices[i+1]], pontos[indices[i+2]], pontos[indices[i+3]], caneta, e);
        }

        int getQtdClicksDesenho()
        {
            if (desenhoSelecionado == 1) // Linha
                return 2;
            if (desenhoSelecionado == 2) // Retangulo
                return 2;
            if (desenhoSelecionado == 3) // Circulo  
                return 1;
            if (desenhoSelecionado == 4) // Elipse
                return 1;
            if (desenhoSelecionado == 5) // Triangulo
                return 3;
            if (desenhoSelecionado == 6) // Losango
                return 4;
            if (desenhoSelecionado == 7) // Pentagono
                return 5;
            return -1;
        }

        void salvarDesenhos()
        {
            // Desenho // Cor // Formato // Espessura // Raio ou Largura e Altura // Coordenadas
            MessageBox.Show("Oi");

            String txt = "" + desenhoSelecionado + " " + corSelecionada + " " + formatoSelecionado + " " + espessuraSelecionada + " ";
            File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", txt);

            if (desenhoSelecionado == 3)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", raio + " ");
            else if (desenhoSelecionado == 4)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", altura + " " + largura + " ");

            for (int i = 0; i < getQtdClicksDesenho() * 2; i++)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", coordenadas[i] + " ");
            File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", "" + Environment.NewLine);
        }

        void definirCor(PaintEventArgs e)
        {
            if (corSelecionada == 1)
                cor = color(0, 0, 0, e);
            else if (corSelecionada == 2)
                cor = color(127, 127, 127, e);
        }

        void definirFormatoLinha(int opc)
        {
            if (opc == 1)
                formatoLinha = new float[1] { 1 };
            else if (opc == 2)
                formatoLinha = new float[2] { 2, 2 };
            else if (opc == 3)
                formatoLinha = new float[2] { 10, 5 };
            else if (opc == 4)
                formatoLinha = new float[4] { 10, 3, 2, 3 };
            else
                formatoLinha = new float[4] { 20, 4, 10, 4 };
        }

        void permitirInvalidate()
        {
            if (clicks == getQtdClicksDesenho())
            {
                validarDesenho = true;
                Invalidate();
            }
        }

        void desenharPaint(Pen caneta, PaintEventArgs e)
        {
            if (validarDesenho)
            {
                if (desenhoSelecionado == 1)
                    linha(coordenadas[0], coordenadas[1], coordenadas[2], coordenadas[3], caneta, e);
                else if (desenhoSelecionado == 2)
                    retangulo(coordenadas, caneta, e);
                else if (desenhoSelecionado == 3)
                    circulo(coordenadas, caneta, e);
                else if (desenhoSelecionado == 4)
                    elipse(coordenadas, caneta, e);
                else if (desenhoSelecionado == 5)
                    triangulo(coordenadas, caneta, e);
                else if (desenhoSelecionado == 6)
                    losango(coordenadas, caneta, e);
                else if (desenhoSelecionado == 7)
                    pentagono(coordenadas, caneta, e);

                salvarDesenhos();

                //marcarCoordenadas = false;
                //validarDesenho = false;
                indiceCoordenadas = 0;
                //tipoDesenho = 0;
                clicks = 0;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            definirCor(e);
            Pen caneta = criarCaneta(cor, formatoLinha, espessura, e);
            desenharPaint(caneta, e);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marcarCoordenadas)
            {
                coordenadas[indiceCoordenadas++] = e.X;
                coordenadas[indiceCoordenadas++] = e.Y;
                clicks++;

                permitirInvalidate();
            }
        }

        // Botão da linha
        private void button1_Click(object sender, EventArgs e)
        {
            marcarCoordenadas = true;
            desenhoSelecionado = 1;
        }

        // Botão do retangulo 
        private void button2_Click(object sender, EventArgs e)
        {
            marcarCoordenadas = true;
            desenhoSelecionado = 2;
        }

        // Botão do circulo 
        private void button3_Click(object sender, EventArgs e)
        {
            // InputBox: Interaction.InputBox("Informe o raio: ", "", "", 400, 400)
            raio = int.Parse(Interaction.InputBox("Informe o raio do círculo: ", "", "", 400, 400));
            marcarCoordenadas = true;
            desenhoSelecionado = 3;
        }

        // Botão da elipse 
        private void button4_Click(object sender, EventArgs e)
        {
            largura = int.Parse(Interaction.InputBox("Informe a largura da elipse: ", "", "", 400, 400));
            altura = int.Parse(Interaction.InputBox("Informe a altura da elipse: ", "", "", 400, 400));
            marcarCoordenadas = true;
            desenhoSelecionado = 4;
        }

        // Botão do triangulo
        private void button5_Click(object sender, EventArgs e)
        {
            marcarCoordenadas = true;
            desenhoSelecionado = 5;
        }

        // Tipo de linha
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            formatoSelecionado = int.Parse(comboBox1.SelectedItem.ToString());
            definirFormatoLinha(formatoSelecionado);
        }

        // Tipo de espessura
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            espessuraSelecionada = int.Parse(comboBox2.SelectedItem.ToString());
            espessura = espessuraSelecionada * 3;
        }

        // Botão do losango
        private void button6_Click(object sender, EventArgs e)
        {
            marcarCoordenadas = true;
            desenhoSelecionado = 6;
        }

        // Botão do pentagono
        private void button7_Click(object sender, EventArgs e)
        {
            marcarCoordenadas = true;
            desenhoSelecionado = 7;
        }
    }
}
