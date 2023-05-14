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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool marcarCoordenadas = false;
        int desenho; // 1 - Reta / 2 - Retangulo / 3 - Circulo / 4 - Elipse / 5 - Trialgulo / 6 - Losango / 7 - Pentagono
        int[] coordenadas = new int[10];
        int indiceCoordenadas = 0;

        Color color(int r, int g, int b, PaintEventArgs e)
        {
            return Color.FromArgb(r, g, b);
        }

        /*Pen caneta(Color cor, int espessura, float[] formatoLinha, PaintEventArgs e)
        {
            Pen caneta = new Pen(cor, espessura);
            caneta.DashPattern = formatoLinha; 
            return caneta;
        }*/
        
        Pen caneta(Color cor, int espessura, PaintEventArgs e) // Só pra testes manuais
        {
            Pen caneta = new Pen(cor, espessura);
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
            e.Graphics.DrawRectangle(caneta, pontos[0], pontos[1], pontos[2], pontos[3]);
        }

        void circulo(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(caneta, pontos[0], pontos[1], pontos[2], pontos[2]);
        }

        void elipse(int[] pontos, Pen caneta, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(caneta, pontos[0], pontos[1], pontos[2], pontos[3]);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int[] p = { 200, 200, 300, 100, 400, 400, 300, 300 }; // teste manual
            Pen objetoCaneta = caneta(color(150, 0, 0, e), 0, e); // teste manual
            pentagono(coordenadas, objetoCaneta, e); // teste manual
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marcarCoordenadas)
            {
                coordenadas[indiceCoordenadas++] = e.X;
                coordenadas[indiceCoordenadas++] = e.Y;
                if (indiceCoordenadas == 10) // Caso pentagono - 10 Coordenadas
                {
                    indiceCoordenadas = 0;
                    Invalidate();
                }
            }
        }
    }
}
