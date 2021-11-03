﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAAD_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cboPacienti.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select numepersoana from persoane, pacient where codpersoana=codpacient";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cboPacienti.Items.Add(dr["numepersoana"]);
            }
            con.Close();





           



        }

        NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=mariana12;Database=Clinica_stomatologica");
        NpgsqlCommand cmd;
        NpgsqlCommand cmd1;
        NpgsqlDataAdapter da;
        NpgsqlDataReader dr;
        NpgsqlDataReader dr1;
        string npgsql;

        private void chrAfectiuni_Click(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
           

        }

        private void cboPacienti_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            cmd = new NpgsqlCommand("select denumireserviciu from persoane inner join pacient on  persoane.codpersoana = pacient.codpacient inner join programare on pacient.codpacient = programare.codpacient inner join serviciiprestate on serviciiprestate.codprogramare = programare.codprogramare inner join serviciistomatologice on serviciistomatologice.codserviciustomatologic = serviciiprestate.codserviciustomatologic where numepersoana='" + cboPacienti.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            NpgsqlDataReader dr;
            int i = 0;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listBox1.Items.Add(dr["denumireserviciu"]);


            }
            con.Close();

            listBox2.Items.Clear();
            cmd1 = new NpgsqlCommand(" select distinct denumireafectiune from persoane inner join pacient on persoane.codpersoana = pacient.codpacient inner join programare on pacient.codpacient = programare.codpacient inner join serviciiprestate on serviciiprestate.codprogramare = programare.codprogramare inner join diagnostice on diagnostice.idserviciuprestat = serviciiprestate.idserviciuprestat inner join afectiuni on afectiuni.codafectiune = diagnostice.codafectiune where numepersoana='" + cboPacienti.Text + "'", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            NpgsqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                listBox2.Items.Add(dr1["denumireafectiune"]);


            }
            con.Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void comboBoxAfectiuni_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

            


            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("select distinct to_char(serviciiprestate.dataserviciuprestat, 'Month') as month, count(pacient.codpacient) as nr_afectiuni from serviciiprestate join factura on factura.codfactura = serviciiprestate.codfactura join pacient on pacient.codpacient = factura.codpacient group by month");

            cmd.ExecuteNonQuery();
            NpgsqlDataReader dr;
            int i = 0;
            dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                chart1.Series["NumarPacienti"].Points.AddXY(dr["month"].ToString(), dr["nr_afectiuni"].ToString());


            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            NpgsqlConnection conexiune;
            string sirConectare;
            sirConectare = "Host=localhost;Username=postgres;Password=mariana12;Database=Clinica_stomatologica";
            conexiune = new NpgsqlConnection();
            conexiune.ConnectionString = sirConectare;
            conexiune.Open();
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter("select numepersoana, datanasterii, gen, adresa from persoane inner join pacient on persoane.codpersoana = pacient.codpacient", conexiune);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            conexiune.Close();
        }
    }
}

