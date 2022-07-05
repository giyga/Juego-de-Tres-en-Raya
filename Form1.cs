namespace La_Vieja
{
    public partial class Form1 : Form
    {
        private bool NuevoJuego;
        //private byte contX, contY = 0;
        private byte[,] EstadosCasillas = new byte[3, 3]; //1 para los X, 2 para los O
        private bool turnoX; //True para los X
        private byte VictoriasX, VictoriasO, Empates; //Para los contadores de victorias
        public enum ModoDeJuego { JvJ, JvIA }; //Determina el modo de juego seleccionado
        private ModoDeJuego ModoSeleccionado; //Una instancia de ese enum
        public bool JugadorEligeX; //Condición que determina si el jugador elige jugar con X o con O contra la IA. True para las X, False para las Y.
        public Form1()
        {
            InitializeComponent();
        }

        private void DeterminarEmpates(byte[,] EstadoCasillas)
        {
            int CasillasLlenas = 0;
            for (int col = 0; col <= EstadoCasillas.GetLength(1) - 1; col++)
            {
                for (int fil = 0; fil <= EstadoCasillas.GetLength(0) - 1; fil++)
                {
                    if (EstadoCasillas[fil, col] == 1 || EstadoCasillas[fil, col] == 2) CasillasLlenas++;
                }
            }
            if (CasillasLlenas == EstadoCasillas.Length)
            {
                MessageBox.Show("Empate!");
                Empates++;
                labelEmpates.Text = $"Empates:    {Empates}";
                EstadoBtn(false);
                btn_NuevaPartida.Enabled = true;
            }
        }

        private void MostrarJuego(bool estado)
        {
            btn_NuevaPartida.Visible = btn_ReiniciaContador.Visible = Casilla1.Visible =
            Casilla2.Visible = Casilla3.Visible = Casilla4.Visible =
            Casilla5.Visible = Casilla6.Visible = Casilla7.Visible = Casilla8.Visible =
            Casilla9.Visible = labelTablaVictorias.Visible = labelVictoriasX.Visible =
            labelVictoriasO.Visible = labelEmpates.Visible = estado;
        }
        private void EstadoBtn(bool Estado)
        {
            Casilla1.Enabled = Casilla2.Enabled = Casilla3.Enabled = Casilla4.Enabled = Casilla5.Enabled = Casilla6.Enabled = Casilla7.Enabled = Casilla8.Enabled
                = Casilla9.Enabled = Estado;
            btn_ReiniciaContador.Enabled = !Estado;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarJuego(false);
        }

        private void btn_NuevaPartida_Click(object sender, EventArgs e)
        {
            NuevoJuego = true;
            btn_NuevaPartida.Enabled = false;
            turnoX = true;
            Casilla1.Text = Casilla2.Text = Casilla3.Text = Casilla4.Text = Casilla5.Text = Casilla6.Text = Casilla7.Text = Casilla8.Text = Casilla9.Text = "";
            ReiniciaCasillas(ref EstadosCasillas);
            EstadoBtn(true);
            if (ModoSeleccionado == ModoDeJuego.JvIA && !JugadorEligeX) OcuparCentro(RevisarLineas, DeterminarEmpates);
        }

        private delegate void Del_VictoriasYEmpates(byte[,] Casillas);
        public void RevisarLineas(byte[,] EstadoCasillas)
        {
            //HORIZONTALES X
            if (NuevoJuego)
            {
                for (int fil = 0; fil < EstadoCasillas.GetLength(0); fil++)
                {
                    for (int col = 0; col < EstadoCasillas.GetLength(1) - 2; col++)
                    {
                        if (EstadoCasillas[fil, col] == 1 && EstadoCasillas[fil, col + 1] == 1 && EstadoCasillas[fil, col + 2] == 1)
                        {
                            MessageBox.Show("Ganan las X!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            btn_NuevaPartida.Enabled = true;
                            VictoriasX++;
                            labelVictoriasX.Text = $"X:   {VictoriasX}";
                            break;
                        }
                        else break;
                    }
                }
            }

            //HORIZONTALES O
            if (NuevoJuego)
            {
                for (int fil = 0; fil < EstadoCasillas.GetLength(0); fil++)
                {
                    for (int col = 0; col < EstadoCasillas.GetLength(1) - 2; col++)
                    {
                        if (EstadoCasillas[fil, col] == 2 && EstadoCasillas[fil, col + 1] == 2 && EstadoCasillas[fil, col + 2] == 2)
                        {
                            MessageBox.Show("Ganan las O!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasO++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasO.Text = $"O:   {VictoriasO}";
                            break;
                        }
                        else break;
                    }
                }
            }

            //VERTICALES X
            if (NuevoJuego)
            {
                for (int col = 0; col < EstadoCasillas.GetLength(1); col++)
                {
                    for (int fil = 0; fil < EstadoCasillas.GetLength(0) - 2; fil++)
                    {
                        if (EstadoCasillas[fil, col] == 1 && EstadoCasillas[fil + 1, col] == 1 && EstadoCasillas[fil + 2, col] == 1)
                        {
                            MessageBox.Show("Ganan las X!");
                            EstadoBtn(false);
                            NuevoJuego = false;

                            VictoriasX++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasX.Text = $"X:   {VictoriasX}";
                            break;
                        }
                        else break;
                    }
                }
            }

            //VERTICALES O
            if (NuevoJuego)
            {
                for (int col = 0; col < EstadoCasillas.GetLength(1); col++)
                {
                    for (int fil = 0; fil < EstadoCasillas.GetLength(0) - 1; fil++)
                    {
                        if (EstadoCasillas[fil, col] == 2 && EstadoCasillas[fil + 1, col] == 2 && EstadoCasillas[fil + 2, col] == 2)
                        {
                            MessageBox.Show("Ganan las O!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasO++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasO.Text = $"O:   {VictoriasO}";
                            break;
                        }
                        else break;
                    }
                }
            }

            //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA) X
            if (NuevoJuego)
            {
                for (int col = 0; col < EstadoCasillas.GetLength(1) - 1; col++)
                {
                    if (EstadoCasillas[col, col] == 1 && EstadoCasillas[col + 1, col + 1] == 1)
                    {
                        if (col + 1 == EstadoCasillas.GetLength(1) - 1)
                        {
                            MessageBox.Show("Ganan las X!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasX++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasX.Text = $"X:   {VictoriasX}";
                            break;
                        }
                    }
                    else break;
                }
            }

            //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA) O


            for (int col = 0; col < EstadoCasillas.GetLength(1) - 1; col++)
            {
                if (NuevoJuego)
                {
                    if (EstadoCasillas[col, col] == 2 && EstadoCasillas[col + 1, col + 1] == 2)
                    {
                        if (col + 1 == EstadoCasillas.GetLength(1) - 1)
                        {
                            MessageBox.Show("Ganan las O!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasO++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasO.Text = $"O:   {VictoriasO}";
                            break;
                        }
                    }
                    col++;
                }
            }

            {
                int fil = EstadoCasillas.GetLength(0) - 1;
                int col = 0;
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR) X
                while (fil >= 0 && col <= EstadoCasillas.GetLength(0) - 1)
                {
                    if (EstadoCasillas[fil, col] == 1 && EstadoCasillas[fil - 1, col + 1] == 1)
                    {
                        if (col + 1 == EstadoCasillas.GetLength(1) - 1)
                        {
                            MessageBox.Show("Ganan las X!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasX++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasX.Text = $"X:   {VictoriasX}";
                            NuevoJuego = false;
                            break;
                        }
                        else
                        {
                            col++;
                            fil--;
                        }
                    }
                    else col = 3;
                }
                fil = EstadoCasillas.GetLength(0) - 1;
                col = 0;
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR) O
                while (fil >= 0 && col <= EstadoCasillas.GetLength(0) - 1)
                {
                    if (EstadoCasillas[fil, col] == 2 && EstadoCasillas[fil - 1, col + 1] == 2)
                    {
                        if (col + 1 == EstadoCasillas.GetLength(1) - 1)
                        {
                            MessageBox.Show("Ganan las O!");
                            EstadoBtn(false);
                            NuevoJuego = false;
                            VictoriasO++;
                            btn_NuevaPartida.Enabled = true;
                            labelVictoriasO.Text = $"O:   {VictoriasO}";
                            NuevoJuego = false;
                            break;
                        }
                        else
                        {
                            col++;
                            fil--;
                        }
                    }
                    else col = 3;
                }
            }
        }

        private void ReiniciaCasillas(ref byte[,] ReiniciaCasillas)
        {
            for (int col = 0; col <= ReiniciaCasillas.GetLength(1) - 1; col++)
            {
                for (int fil = 0; fil <= ReiniciaCasillas.GetLength(0) - 1; fil++)
                {
                    ReiniciaCasillas[fil, col] = 0;
                }
            }
        }


        #region clics de casillas
        private void button1_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla1.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla1.Text = "X";
                        EstadosCasillas[0, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla1.Text = "O";
                        EstadosCasillas[0, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla1.Text = "X";
                        EstadosCasillas[0, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla1.Text = "O";
                        EstadosCasillas[0, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla2.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla2.Text = "X";
                        EstadosCasillas[0, 1] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla2.Text = "O";
                        EstadosCasillas[0, 1] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla2.Text = "X";
                        EstadosCasillas[0, 1] = 1;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        turnoX = false;
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla2.Text = "O";
                        EstadosCasillas[0, 1] = 2;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        turnoX = true;
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla3.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla3.Text = "X";
                        EstadosCasillas[0, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla3.Text = "O";
                        EstadosCasillas[0, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla3.Text = "X";
                        EstadosCasillas[0, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla3.Text = "O";
                        EstadosCasillas[0, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla4.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla4.Text = "X";
                        EstadosCasillas[1, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla4.Text = "O";
                        EstadosCasillas[1, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla4.Text = "X";
                        EstadosCasillas[1, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla4.Text = "O";
                        EstadosCasillas[1, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla5.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla5.Text = "X";
                        EstadosCasillas[1, 1] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla5.Text = "O";
                        EstadosCasillas[1, 1] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla5.Text = "X";
                        EstadosCasillas[1, 1] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla5.Text = "O";
                        EstadosCasillas[1, 1] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla6.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla6.Text = "X";
                        EstadosCasillas[1, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla6.Text = "O";
                        EstadosCasillas[1, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla6.Text = "X";
                        EstadosCasillas[1, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla6.Text = "O";
                        EstadosCasillas[1, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla7.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla7.Text = "X";
                        EstadosCasillas[2, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla7.Text = "O";
                        EstadosCasillas[2, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla7.Text = "X";
                        EstadosCasillas[2, 0] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla7.Text = "O";
                        EstadosCasillas[2, 0] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla8.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla8.Text = "X";
                        EstadosCasillas[2, 1] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla8.Text = "O";
                        EstadosCasillas[2, 1] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla8.Text = "X";
                        EstadosCasillas[2, 1] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla8.Text = "O";
                        EstadosCasillas[2, 1] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (NuevoJuego == true && String.IsNullOrEmpty(Casilla9.Text))
            {
                if (ModoSeleccionado == ModoDeJuego.JvJ)
                {
                    if (turnoX)
                    {
                        Casilla9.Text = "X";
                        EstadosCasillas[2, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                    else
                    {
                        Casilla9.Text = "O";
                        EstadosCasillas[2, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                    }
                }
                else
                {
                    if (JugadorEligeX)
                    {
                        Casilla9.Text = "X";
                        EstadosCasillas[2, 2] = 1;
                        turnoX = false;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                    else
                    {
                        Casilla9.Text = "O";
                        EstadosCasillas[2, 2] = 2;
                        turnoX = true;
                        RevisarLineas(EstadosCasillas);
                        DeterminarEmpates(EstadosCasillas);
                        OcuparCentro(RevisarLineas, DeterminarEmpates);
                        AtaqueBasico(RevisarLineas);
                        DefensaBasica(DeterminarEmpates);
                        TomarEsquina(RevisarLineas, DeterminarEmpates);
                        JugadaRandom(RevisarLineas, DeterminarEmpates);
                    }
                }
            }
        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e) //Botón de reiniciar contador
        {
            VictoriasX = VictoriasO = Empates = 0;
            labelVictoriasO.Text = $"O:   {VictoriasO}";
            labelVictoriasX.Text = $"X:   {VictoriasX}";
            labelEmpates.Text = $"Empates:    {Empates}";
        }


        #region Selección de Modo de Juego
        private void jugadorVsJugadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VictoriasX = VictoriasO = Empates = 0;
            labelVictoriasO.Text = $"O:   {VictoriasO}";
            labelVictoriasX.Text = $"X:   {VictoriasX}";
            labelEmpates.Text = $"Empates:    {Empates}";
            NuevoJuego = true;
            ModoSeleccionado = ModoDeJuego.JvJ;
            btn_NuevaPartida.Enabled = false;
            turnoX = true;
            Casilla1.Text = Casilla2.Text = Casilla3.Text = Casilla4.Text = Casilla5.Text = Casilla6.Text = Casilla7.Text = Casilla8.Text = Casilla9.Text = "";
            ReiniciaCasillas(ref EstadosCasillas);
            EstadoBtn(true);
            MostrarJuego(true);
        }

        private void jugadorVsIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModoSeleccionado = ModoDeJuego.JvIA;
            DialogResult Respuesta = MessageBox.Show("Jugar con las X?", "Escoge tu figura", MessageBoxButtons.YesNo);
            if (Respuesta == DialogResult.Yes)
            {
                JugadorEligeX = true;
                VictoriasX = VictoriasO = Empates = 0;
                labelVictoriasO.Text = $"O:   {VictoriasO}";
                labelVictoriasX.Text = $"X:   {VictoriasX}";
                labelEmpates.Text = $"Empates:    {Empates}";
                NuevoJuego = true;
                turnoX = true;
                btn_NuevaPartida.Enabled = false;
                Casilla1.Text = Casilla2.Text = Casilla3.Text = Casilla4.Text = Casilla5.Text = Casilla6.Text = Casilla7.Text = Casilla8.Text = Casilla9.Text = "";
                ReiniciaCasillas(ref EstadosCasillas);
                EstadoBtn(true);
                MostrarJuego(true);
            }
            else
            {
                JugadorEligeX = false;
                VictoriasX = VictoriasO = Empates = 0;
                labelVictoriasO.Text = $"O:   {VictoriasO}";
                labelVictoriasX.Text = $"X:   {VictoriasX}";
                labelEmpates.Text = $"Empates:    {Empates}";
                NuevoJuego = true;
                btn_NuevaPartida.Enabled = false;
                Casilla1.Text = Casilla2.Text = Casilla3.Text = Casilla4.Text = Casilla5.Text = Casilla6.Text = Casilla7.Text = Casilla8.Text = Casilla9.Text = "";
                ReiniciaCasillas(ref EstadosCasillas);
                EstadoBtn(true);
                MostrarJuego(true);
                turnoX = true;
                OcuparCentro(RevisarLineas, DeterminarEmpates);
            }
        }



        #endregion



        #region IA
        //NOTA PERSONAL: Si bien verificar victorias y empates no es algo que le competa a la IA, los voy a poner de todas maneras, para poner en práctica los delegados.
        private void OcuparCentro(Del_VictoriasYEmpates Victorias, Del_VictoriasYEmpates Empates)
        {
            //Ver si el centro está ocupado
            if (EstadosCasillas[1, 1] == 0)
            {
                if (JugadorEligeX && !turnoX)
                {
                    EstadosCasillas[1, 1] = 2;
                    Casilla5.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                    Empates(EstadosCasillas);

                }
                else
                {
                    EstadosCasillas[1, 1] = 1;
                    Casilla5.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                    Empates(EstadosCasillas);
                }
            }
        }

        private void AtaqueBasico(Del_VictoriasYEmpates Victorias) //Bloquea o termina de llenar líneas
        {

            if (JugadorEligeX && !turnoX)
            {
                //Horizontales
                for (int fil = 0; fil <= EstadosCasillas.GetLength(0) - 1; fil++)
                {
                    if (EstadosCasillas[fil, 0] == 2 && EstadosCasillas[fil, 1] == 2 && EstadosCasillas[fil, 2] == 0)
                    {
                        EstadosCasillas[fil, 2] = 2;
                        switch (fil)
                        {
                            case 0:
                                Casilla3.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla6.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[fil, 1] == 2 && EstadosCasillas[fil, 2] == 2 && EstadosCasillas[fil, 0] == 0) 
                    {
                        EstadosCasillas[fil, 0] = 2;
                        switch (fil) //NOTA: Necesito llamar a la función RevisarLineas en cada uno de estos casos para evitar reciclar código
                        {
                            case 0:
                                Casilla1.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla4.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla7.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }

                    }
                    else if (EstadosCasillas[fil, 0] == 2 && EstadosCasillas[fil, 2] == 2 && EstadosCasillas[fil, 1] == 0)
                    {
                        EstadosCasillas[fil, 1] = 2;
                        switch (fil)
                        {
                            case 0:
                                Casilla2.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla8.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    //else break;
                }

                //Verticales
                for (int col = 0; col < EstadosCasillas.GetLength(1); col++)
                {
                    if (EstadosCasillas[0, col] == 2 && EstadosCasillas[1, col] == 2 && EstadosCasillas[2, col] == 0)
                    {
                        EstadosCasillas[2, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla7.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla8.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[1, col] == 2 && EstadosCasillas[2, col] == 2 && EstadosCasillas[0, col] == 0)
                    {
                        EstadosCasillas[0, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla1.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla2.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla3.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[0, col] == 2 && EstadosCasillas[2, col] == 2 && EstadosCasillas[1, col] == 0)
                    {
                        EstadosCasillas[1, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla4.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla6.Text = "O";
                                turnoX = true;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    //else break;
                }
                //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA)
                if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 2] == 0)
                {
                    EstadosCasillas[2, 2] = 2;
                    Casilla9.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[0, 0] == 0)
                {
                    EstadosCasillas[0, 0] = 2;
                    Casilla1.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[1, 1] == 0)
                {
                    EstadosCasillas[1, 1] = 2;
                    Casilla5.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR)

                if (EstadosCasillas[0, 2] == 2 && EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 0] == 0)
                {
                    EstadosCasillas[2, 0] = 2;
                    Casilla7.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 0] == 2 && EstadosCasillas[0, 2] == 0)
                {
                    EstadosCasillas[0, 2] = 2;
                    Casilla3.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[1, 1] == 0)
                {
                    EstadosCasillas[1, 1] = 2;
                    Casilla5.Text = "O";
                    turnoX = true;
                    Victorias(EstadosCasillas);
                }
            }
            else if (!JugadorEligeX && turnoX) //Si el jugador elige jugar por las O
            {
                //Horizontales
                for (int fil = 0; fil < EstadosCasillas.GetLength(0); fil++)
                {
                    if (EstadosCasillas[fil, 0] == 1 && EstadosCasillas[fil, 1] == 1 && EstadosCasillas[fil, 2] == 0)
                    {
                        EstadosCasillas[fil, 2] = 1;
                        switch (fil)
                        {
                            case 0:
                                Casilla3.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla6.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                        }

                    }
                    else if (EstadosCasillas[fil, 1] == 1 && EstadosCasillas[fil, 2] == 1 && EstadosCasillas[fil, 0] == 0)
                    {

                        EstadosCasillas[fil, 0] = 1;
                        switch (fil) 
                        {
                            case 0:
                                Casilla1.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);

                                break;
                            case 1:
                                Casilla4.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);

                                break;
                            case 2:
                                Casilla7.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                        }

                    }
                    else if (EstadosCasillas[fil, 0] == 1 && EstadosCasillas[fil, 2] == 1 && EstadosCasillas[fil, 1] == 0)
                    {
                        if (EstadosCasillas[fil, 0] == 1)
                        {
                            EstadosCasillas[fil, 1] = 1;
                            switch (fil)
                            {
                                case 0:
                                    Casilla2.Text = "X";
                                    turnoX = false;
                                    Victorias(EstadosCasillas);
                                    break;
                                case 1:
                                    Casilla5.Text = "X";
                                    turnoX = false;
                                    Victorias(EstadosCasillas);
                                    break;
                                case 2:
                                    Casilla8.Text = "X";
                                    turnoX = false;
                                    Victorias(EstadosCasillas);
                                    break;
                            }
                        }
                    }
                    //else break;
                }

                //Verticales
                for (int col = 0; col < EstadosCasillas.GetLength(1); col++)
                {
                    if (EstadosCasillas[0, col] == 1 && EstadosCasillas[1, col] == 1 && EstadosCasillas[2, col] == 0)
                    {

                        EstadosCasillas[2, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla7.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla8.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[1, col] == 1 && EstadosCasillas[2, col] == 1 && EstadosCasillas[0, col] == 0)
                    {
                        EstadosCasillas[0, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla1.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla2.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla3.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[0, col] == 1 && EstadosCasillas[2, col] == 1 && EstadosCasillas[1, col] == 0)
                    {
                        EstadosCasillas[1, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla4.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                            case 2:
                                Casilla6.Text = "X";
                                turnoX = false;
                                Victorias(EstadosCasillas);
                                break;
                        }
                    }
                    //else break;
                }
                //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA)
                if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 2] == 0)
                {
                    EstadosCasillas[2, 2] = 1;
                    Casilla9.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[0, 0] == 0)
                {
                    EstadosCasillas[0, 0] = 1;
                    Casilla1.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[1, 1] == 0)
                {
                    EstadosCasillas[1, 1] = 1;
                    Casilla5.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR)

                if (EstadosCasillas[0, 2] == 1 && EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 0] == 0)
                {
                    EstadosCasillas[2, 0] = 1;
                    Casilla7.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 0] == 1 && EstadosCasillas[0, 2] == 0)
                {
                    EstadosCasillas[0, 2] = 1;
                    Casilla3.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[1, 1] == 0)
                {
                    EstadosCasillas[1, 1] = 1;
                    Casilla5.Text = "X";
                    turnoX = false;
                    Victorias(EstadosCasillas);
                }
            }
        }



        private void DefensaBasica(Del_VictoriasYEmpates Empates)
        {
            if (JugadorEligeX && !turnoX)
            {
                //Jugada predictiva
                byte CasillasDisponibles = 0;
                for(int fil = 0; fil <= EstadosCasillas.GetLength(0) - 1; fil++)
                {
                    for(int col = 0; col <= EstadosCasillas.GetLength(1) - 1; col++)
                    {
                        if (EstadosCasillas[fil, col] == 0) CasillasDisponibles++;
                    }
                }
                if (CasillasDisponibles == 6 && ((EstadosCasillas[0,0] == 1 && EstadosCasillas[2,2] == 1) || (EstadosCasillas[2, 0] == 1 && EstadosCasillas[0, 2] == 1)))
                {
                    Random rnd = new Random();
                    byte Cruces = (byte)rnd.Next(1, 5);
                    switch(Cruces)
                    {
                        case 1:
                            Casilla2.Text = "O";
                            EstadosCasillas[0, 1] = 2;
                            turnoX = true;
                            break;
                        case 2:
                            Casilla4.Text = "O";
                            EstadosCasillas[1, 0] = 2;
                            turnoX = true;
                            break;
                        case 3:
                            Casilla6.Text = "O";
                            EstadosCasillas[1, 2] = 2;
                            turnoX = true;
                            break;
                        case 4:
                            Casilla8.Text = "O";
                            EstadosCasillas[2, 1] = 2;
                            turnoX = true;
                            break;
                    }
                }

                //Horizontales
                for (int fil = 0; fil < EstadosCasillas.GetLength(0); fil++)
                {
                    if (EstadosCasillas[fil, 0] == 1 && EstadosCasillas[fil, 1] == 1 && EstadosCasillas[fil, 2] == 0 && !turnoX)
                    {
                        EstadosCasillas[fil, 2] = 2;
                        switch (fil)
                        {
                            case 0:
                                Casilla3.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla6.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[fil, 1] == 1 && EstadosCasillas[fil, 2] == 1 && EstadosCasillas[fil, 0] == 0 && !turnoX)
                    {
                        EstadosCasillas[fil, 0] = 2;
                        switch (fil) //NOTA: Necesito llamar a la función RevisarLineas en cada uno de estos casos para evitar reciclar código
                        {
                            case 0:
                                Casilla1.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla4.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla7.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }

                    }
                    else if (EstadosCasillas[fil, 0] == 1 && EstadosCasillas[fil, 2] == 1 && EstadosCasillas[fil, 1] == 0 && !turnoX)
                    {
                        EstadosCasillas[fil, 1] = 2;
                        switch (fil)
                        {
                            case 0:
                                Casilla2.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla8.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }

                    }
                    //else break;
                }

                //Verticales
                for (int col = 0; col < EstadosCasillas.GetLength(1); col++)
                {
                    if (EstadosCasillas[0, col] == 1 && EstadosCasillas[1, col] == 1 && EstadosCasillas[2, col] == 0 && !turnoX)
                    {
                        EstadosCasillas[2, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla7.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla8.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[1, col] == 1 && EstadosCasillas[2, col] == 1 && EstadosCasillas[0, col] == 0 && !turnoX)
                    {
                        EstadosCasillas[0, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla1.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla2.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla3.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[0, col] == 1 && EstadosCasillas[2, col] == 1 && EstadosCasillas[1, col] == 0 && !turnoX)
                    {
                        EstadosCasillas[1, col] = 2;
                        switch (col)
                        {
                            case 0:
                                Casilla4.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla6.Text = "O";
                                turnoX = true;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    //else break;
                }
                //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA)
                if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 2] == 0 && !turnoX)
                {
                    EstadosCasillas[2, 2] = 2;
                    Casilla9.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[0, 0] == 0 && !turnoX)
                {
                    EstadosCasillas[0, 0] = 2;
                    Casilla1.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[1, 1] == 0 && !turnoX)
                {
                    EstadosCasillas[1, 1] = 2;
                    Casilla5.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR)

                if (EstadosCasillas[0, 2] == 1 && EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 0] == 0 && !turnoX)
                {
                    EstadosCasillas[2, 0] = 2;
                    Casilla7.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 1 && EstadosCasillas[2, 0] == 1 && EstadosCasillas[0, 2] == 0 && !turnoX)
                {
                    EstadosCasillas[0, 2] = 2;
                    Casilla3.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 1 && EstadosCasillas[2, 2] == 1 && EstadosCasillas[1, 1] == 0 && !turnoX)
                {
                    EstadosCasillas[1, 1] = 2;
                    Casilla5.Text = "O";
                    turnoX = true;
                    Empates(EstadosCasillas);
                }
            }
            else if (!JugadorEligeX && turnoX) //Si el jugador elige jugar por las O
            {
                //Horizontales
                for (int fil = 0; fil < EstadosCasillas.GetLength(0); fil++)
                {
                    if (EstadosCasillas[fil, 0] == 2 && EstadosCasillas[fil, 1] == 2 && EstadosCasillas[fil, 2] == 0 && turnoX)
                    {
                        EstadosCasillas[fil, 2] = 1;
                        switch (fil)
                        {
                            case 0:
                                Casilla3.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla6.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[fil, 1] == 2 && EstadosCasillas[fil, 2] == 2 && EstadosCasillas[fil, 0] == 0 && turnoX)
                    {
                        EstadosCasillas[fil, 0] = 1;
                        switch (fil) //NOTA: Necesito llamar a la función RevisarLineas en cada uno de estos casos para evitar reciclar código
                        {
                            case 0:
                                Casilla1.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla4.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla7.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[fil, 0] == 2 && EstadosCasillas[fil, 2] == 2 && EstadosCasillas[fil, 1] == 0 && turnoX)
                    {
                        EstadosCasillas[fil, 1] = 1;
                        switch (fil)
                        {
                            case 0:
                                Casilla2.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla8.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }

                    }
                    //else break;
                }

                //Verticales
                for (int col = 0; col < EstadosCasillas.GetLength(1); col++)
                {
                    if (EstadosCasillas[0, col] == 2 && EstadosCasillas[1, col] == 2 && EstadosCasillas[2, col] == 0 && turnoX)
                    {

                        EstadosCasillas[2, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla7.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla8.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla9.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[1, col] == 2 && EstadosCasillas[2, col] == 2 && EstadosCasillas[0, col] == 0 && turnoX)
                    {
                        EstadosCasillas[0, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla1.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla2.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla3.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    else if (EstadosCasillas[0, col] == 2 && EstadosCasillas[2, col] == 2 && EstadosCasillas[1, col] == 0 && turnoX)
                    {
                        EstadosCasillas[1, col] = 1;
                        switch (col)
                        {
                            case 0:
                                Casilla4.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 1:
                                Casilla5.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                            case 2:
                                Casilla6.Text = "X";
                                turnoX = false;
                                Empates(EstadosCasillas);
                                break;
                        }
                    }
                    //else break;
                }
                //DIAGONALES 1 (IZQ-ARR HASTA DER-ABA)
                if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 2] == 0 && turnoX)
                {
                    EstadosCasillas[2, 2] = 1;
                    Casilla9.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[0, 0] == 0 && turnoX)
                {
                    EstadosCasillas[0, 0] = 1;
                    Casilla1.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[1, 1] == 0 && turnoX)
                {
                    EstadosCasillas[1, 1] = 1;
                    Casilla5.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
                //DIAGONALES 2 (IZQ-ABA HASTA DER-ARR)

                if (EstadosCasillas[0, 2] == 2 && EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 0] == 0 && turnoX)
                {
                    EstadosCasillas[2, 0] = 1;
                    Casilla7.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[1, 1] == 2 && EstadosCasillas[2, 0] == 2 && EstadosCasillas[0, 2] == 0 && turnoX)
                {
                    EstadosCasillas[0, 2] = 1;
                    Casilla3.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
                else if (EstadosCasillas[0, 0] == 2 && EstadosCasillas[2, 2] == 2 && EstadosCasillas[1, 1] == 0 && turnoX)
                {
                    EstadosCasillas[1, 1] = 1;
                    Casilla5.Text = "X";
                    turnoX = false;
                    Empates(EstadosCasillas);
                }
            }
        }

        private void TomarEsquina(Del_VictoriasYEmpates Victorias, Del_VictoriasYEmpates Empates) //Falta desactivar o activar turnoX
        {

            if (EstadosCasillas[0, 0] == 0 || EstadosCasillas[0, 2] == 0 || EstadosCasillas[2, 0] == 0 || EstadosCasillas[2, 2] == 0) //Ver si hay esquinas disponibles para así aplicar esta función
            {
                if (JugadorEligeX && !turnoX)
                {
                    Random rnd = new Random();
                    bool EsquinaHallada = false;
                    while (!EsquinaHallada)
                    {
                        int Esquina;
                        Esquina = rnd.Next(1, 5);
                        if (Esquina == 1 && EstadosCasillas[0, 0] == 0)
                        {
                            EstadosCasillas[0, 0] = 2;
                            Casilla1.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 2 && EstadosCasillas[0, 2] == 0)
                        {
                            EstadosCasillas[0, 2] = 2;
                            Casilla3.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 3 && EstadosCasillas[2, 0] == 0)
                        {
                            EstadosCasillas[2, 0] = 2;
                            Casilla7.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 4 && EstadosCasillas[2, 2] == 0)
                        {
                            EstadosCasillas[2, 2] = 2;
                            Casilla9.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            EsquinaHallada = true;
                        }
                    }
                }
                else if (!JugadorEligeX && turnoX)//Si el jugador elige jugar con las O
                {
                    Random rnd = new Random();
                    bool EsquinaHallada = false;
                    while (!EsquinaHallada)
                    {
                        int Esquina;
                        Esquina = rnd.Next(1, 5);
                        if (Esquina == 1 && EstadosCasillas[0, 0] == 0)
                        {
                            EstadosCasillas[0, 0] = 1;
                            Casilla1.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 2 && EstadosCasillas[0, 2] == 0)
                        {
                            EstadosCasillas[0, 2] = 1;
                            Casilla3.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 3 && EstadosCasillas[2, 0] == 0)
                        {
                            EstadosCasillas[2, 0] = 1;
                            Casilla7.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            EsquinaHallada = true;
                        }
                        if (Esquina == 4 && EstadosCasillas[2, 2] == 0)
                        {
                            EstadosCasillas[2, 2] = 1;
                            Casilla9.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            EsquinaHallada = true;
                        }
                    }
                }
            }
        }

        private void JugadaRandom(Del_VictoriasYEmpates Victorias, Del_VictoriasYEmpates Empates) // luego le pongo el modificador de acceso
        {

            bool QuedanCasillas = false;
            for (int fil = 0; fil <= EstadosCasillas.GetLength(0) - 1; fil++)
            {               
                for (int col = 0; col <= EstadosCasillas.GetLength(1) - 1; col++)
                {
                    if (EstadosCasillas[fil, col] == 0)
                    {
                        QuedanCasillas = true;
                        break;
                    } 
                }
                if (QuedanCasillas) break;
            }
            if (JugadorEligeX && !turnoX && QuedanCasillas)
            {
                    Random rnd = new Random();
                    bool CasillaHallada = false;
                    while (!CasillaHallada)
                    {
                        int CasillaRandom;
                        CasillaRandom = rnd.Next(1, 10);
                        if (CasillaRandom == 1 && EstadosCasillas[0, 0] == 0)
                        {
                            EstadosCasillas[0, 0] = 2;
                            Casilla1.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 2 && EstadosCasillas[0, 1] == 0)
                        {
                            EstadosCasillas[0, 1] = 2;
                            Casilla2.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 3 && EstadosCasillas[0, 2] == 0)
                        {
                            EstadosCasillas[0, 2] = 2;
                            Casilla3.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 4 && EstadosCasillas[1, 0] == 0)
                        {
                            EstadosCasillas[1, 0] = 2;
                            Casilla4.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 5 && EstadosCasillas[1, 1] == 0)
                        {
                            EstadosCasillas[1, 1] = 2;
                            Casilla5.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 6 && EstadosCasillas[1, 2] == 0)
                        {
                            EstadosCasillas[1, 2] = 2;
                            Casilla6.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 7 && EstadosCasillas[2, 0] == 0)
                        {
                            EstadosCasillas[2, 0] = 2;
                            Casilla7.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 8 && EstadosCasillas[2, 1] == 0)
                        {
                            EstadosCasillas[2, 1] = 2;
                            Casilla8.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 9 && EstadosCasillas[2, 2] == 0)
                        {
                            EstadosCasillas[2, 2] = 2;
                            Casilla9.Text = "O";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = true;
                            CasillaHallada = true;
                        }
                    }                
            }
            else if(!JugadorEligeX && turnoX && QuedanCasillas)
            {
                    Random rnd = new Random();
                    bool CasillaHallada = false;
                    while (!CasillaHallada)
                    {
                        int CasillaRandom;
                        CasillaRandom = rnd.Next(1, 10);
                        if (CasillaRandom == 1 && EstadosCasillas[0, 0] == 0)
                        {
                            EstadosCasillas[0, 0] = 1;
                            Casilla1.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 2 && EstadosCasillas[0, 1] == 0)
                        {
                            EstadosCasillas[0, 1] = 1;
                            Casilla2.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 3 && EstadosCasillas[0, 2] == 0)
                        {
                            EstadosCasillas[0, 2] = 1;
                            Casilla3.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 4 && EstadosCasillas[1, 0] == 0)
                        {
                            EstadosCasillas[1, 0] = 1;
                            Casilla4.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 5 && EstadosCasillas[1, 1] == 0)
                        {
                            EstadosCasillas[1, 1] = 2;
                            Casilla5.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 6 && EstadosCasillas[1, 2] == 0)
                        {
                            EstadosCasillas[1, 2] = 1;
                            Casilla6.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 7 && EstadosCasillas[2, 0] == 0)
                        {
                            EstadosCasillas[2, 0] = 1;
                            Casilla7.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 8 && EstadosCasillas[2, 1] == 0)
                        {
                            EstadosCasillas[2, 1] = 1;
                            Casilla8.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                        if (CasillaRandom == 9 && EstadosCasillas[2, 2] == 0)
                        {
                            EstadosCasillas[2, 2] = 1;
                            Casilla9.Text = "X";
                            Victorias(EstadosCasillas);
                            Empates(EstadosCasillas);
                            turnoX = false;
                            CasillaHallada = true;
                        }
                    }
            }

        }

        #endregion

    }
}