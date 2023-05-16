﻿/* Colegio Técnico Antônio Teixeira Fernandes (Univap)
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

namespace projeto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool desenharSalvos = false;
        bool marcarCoordenadas = false;
        bool validarDesenho = false;
        int desenhoSelecionado = 0; // 1 - Reta / 2 - Retangulo / 3 - Circulo / 4 - Elipse / 5 - Trialgulo / 6 - Losango / 7 - Pentagono
        int clicks = 0;
        int[] coordenadas = new int[10];
        int raio, altura, largura; // Caso da elipse e circulo
        int indiceCoordenadas = 0;
        int formatoSelecionado = 1;
        float[] formatoLinha = { 1 };
        int espessuraSelecionada = 1;
        int espessura = 1;
        int corSelecionada = 1;
        int[] RGB = new int[3] { 0, 0, 0 };

        Color color(int r, int g, int b, PaintEventArgs e)
        {
            return Color.FromArgb(r, g, b);
        }
        
        Pen criarCaneta(Color cor, float[] fLinha, int espessura, PaintEventArgs e)
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
            linha(pontos[0], pontos[1], pontos[2], pontos[1], caneta, e);
            linha(pontos[2], pontos[1], pontos[2], pontos[3], caneta, e);
            linha(pontos[2], pontos[3], pontos[0], pontos[3], caneta, e);
            linha(pontos[0], pontos[3], pontos[0], pontos[1], caneta, e);
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

        void definirFormatoLinha()
        {
            if (formatoSelecionado == 1)
                formatoLinha = new float[1] { 1 };
            else if (formatoSelecionado == 2)
                formatoLinha = new float[2] { 2, 2 };
            else if (formatoSelecionado == 3)
                formatoLinha = new float[2] { 10, 5 };
            else if (formatoSelecionado == 4)
                formatoLinha = new float[4] { 10, 3, 2, 3 };
            else
                formatoLinha = new float[4] { 20, 4, 10, 4 };
        }

        void definirCor()
        {
            if (corSelecionada == 1)
                RGB = new int[3] { 0, 0, 0 };
            else if (corSelecionada == 2)
                RGB = new int[3] { 255, 255, 255 };
            else if (corSelecionada == 3)
                RGB = new int[3] { 127, 127, 127 };
            else if (corSelecionada == 4)
                RGB = new int[3] { 195, 195, 195 };
            else if (corSelecionada == 5)
                RGB = new int[3] { 136, 0, 21 };
            else if (corSelecionada == 6)
                RGB = new int[3] { 185, 122, 87 };
            else if (corSelecionada == 7)
                RGB = new int[3] { 237, 28, 36 };
            else if (corSelecionada == 8)
                RGB = new int[3] { 255, 174, 201 };
            else if (corSelecionada == 9)
                RGB = new int[3] { 255, 127, 39 };
            else if (corSelecionada == 10)
                RGB = new int[3] { 255, 201, 14 };
            else if (corSelecionada == 11)
                RGB = new int[3] { 255, 242, 0 };
            else if (corSelecionada == 12)
                RGB = new int[3] { 239, 228, 176 };
            else if (corSelecionada == 13)
                RGB = new int[3] { 34, 177, 76 };
            else if (corSelecionada == 14)
                RGB = new int[3] { 181, 230, 29 };
            else if (corSelecionada == 15)
                RGB = new int[3] { 0, 162, 232 };
            else if (corSelecionada == 16)
                RGB = new int[3] { 153, 217, 234 };
            else if (corSelecionada == 17)
                RGB = new int[3] { 63, 72, 204 };
            else if (corSelecionada == 18)
                RGB = new int[3] { 112, 146, 190 };
            else if (corSelecionada == 19)
                RGB = new int[3] { 163, 73, 164 };
            else
                RGB = new int[3] { 200, 191, 231 };
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

        void permitirInvalidate()
        {
            if (clicks == getQtdClicksDesenho())
            {
                validarDesenho = true;
                Invalidate();
            }
        }

        void desenharPaint(PaintEventArgs e)
        {
            if (validarDesenho || desenharSalvos)
            {
                definirCor();
                definirFormatoLinha();
                Color cor = color(RGB[0], RGB[1], RGB[2], e);
                Pen caneta = criarCaneta(cor, formatoLinha, espessura, e);

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

                if(!desenharSalvos)
                    salvarDesenhos();

                //marcarCoordenadas = false;
                //tipoDesenho = 0;
                validarDesenho = false;
                indiceCoordenadas = 0;
                clicks = 0;
                desenharSalvos = true;
            }
        }

        void salvarDesenhos()
        {
            // Desenho // Cor // Formato // Espessura // Raio ou Largura e Altura // Coordenadas

            String txt = "" + desenhoSelecionado + " " + corSelecionada + " " + formatoSelecionado + " " + espessuraSelecionada + " ";
            File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", txt);

            if (desenhoSelecionado == 3)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", raio + " ");
            else if (desenhoSelecionado == 4)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", largura + " " + altura + " ");

            for (int i = 0; i < getQtdClicksDesenho() * 2; i++)
                File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", coordenadas[i] + " ");
            File.AppendAllText(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt", "" + Environment.NewLine);
        }

        void pintarDesenhosSalvos(PaintEventArgs e)
        {
            // Desenho // Cor // Formato // Espessura // Raio ou Largura e Altura // Coordenadas

            if (File.Exists(@"C:\\Users\\user\\Documents\\INFORMATICA\\PROJETOS\\ICG\\2 BIMESTRE\\dados.txt"))
            {
                String[] desenhos = File.ReadAllLines(@"C:\Users\user\Documents\INFORMATICA\PROJETOS\ICG\2 BIMESTRE\dados.txt");

                if (desenhos.Length > 0)
                {
                    foreach (String desenho in desenhos)
                    {
                        int i = 0;
                        String[] dadosDesenho = desenho.Split(' ');

                        desenhoSelecionado = int.Parse(dadosDesenho[i++]);
                        corSelecionada = int.Parse(dadosDesenho[i++]);
                        formatoSelecionado = int.Parse(dadosDesenho[i++]);
                        espessura = int.Parse(dadosDesenho[i++]) * 3;

                        if (desenhoSelecionado == 3)
                            raio = int.Parse(dadosDesenho[i++]);
                        else if (desenhoSelecionado == 4)
                        {
                            largura = int.Parse(dadosDesenho[i++]);
                            altura = int.Parse(dadosDesenho[i++]);
                        }

                        for (int j = 0; j < getQtdClicksDesenho() * 2; j++)
                            coordenadas[j] = int.Parse(dadosDesenho[i++]);

                        desenharPaint(e);
                    }
                }
                desenharSalvos = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            desenharPaint(e);
            if (desenharSalvos)
                pintarDesenhosSalvos(e);
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
        }

        // Tipo de espessura
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            espessuraSelecionada = int.Parse(comboBox2.SelectedItem.ToString());
            espessura = espessuraSelecionada * 3;
        }

        private void button8_Click(object sender, EventArgs e) //cor preta
        {
            corSelecionada = 1;
        }

        private void button9_Click(object sender, EventArgs e) //cor branca
        {
            corSelecionada = 2;
        }

        private void button10_Click(object sender, EventArgs e) //cor cinza 
        {
            corSelecionada = 3;            
        }

        private void button11_Click(object sender, EventArgs e) //cor cinza claro
        {
            corSelecionada = 4;            
        }

        private void button12_Click(object sender, EventArgs e) //vermelho escuro
        {
            corSelecionada = 5;
        }

        private void button13_Click(object sender, EventArgs e) //marrom
        {
            corSelecionada = 6;            
        }

        private void button14_Click(object sender, EventArgs e) //vermelho
        {
            corSelecionada = 7;            
        }

        private void button15_Click(object sender, EventArgs e) //rosa
        {
            corSelecionada = 8;            
        }

        private void button16_Click(object sender, EventArgs e) //laranja
        {
            corSelecionada = 9;            
        }

        private void button17_Click(object sender, EventArgs e) //dourado
        {
            corSelecionada = 10;            
        }

        private void button18_Click(object sender, EventArgs e) //amarelo
        {
            corSelecionada = 11;            
        }

        private void button19_Click(object sender, EventArgs e) //amarelo claro
        {
            corSelecionada = 12;            
        }

        private void button20_Click(object sender, EventArgs e) //verde
        {
            corSelecionada = 13;            
        }

        private void button21_Click(object sender, EventArgs e) //lima
        {
            corSelecionada = 14;            
        }

        private void button22_Click(object sender, EventArgs e) //turquesa
        {
            corSelecionada = 15;            
        }

        private void button23_Click(object sender, EventArgs e) //turquesa claro
        {
            corSelecionada = 16;            
        }

        private void button24_Click(object sender, EventArgs e) //índigo
        {
            corSelecionada = 17;            
        }

        private void button25_Click(object sender, EventArgs e) //cinza azulado
        {
            corSelecionada = 18;            
        }

        private void button26_Click(object sender, EventArgs e) //roxo
        {
            corSelecionada = 19;            
        }

        private void button27_Click(object sender, EventArgs e) //lavanda
        {
            corSelecionada = 20;            
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
