﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=TUNCER-SSD\SQLEXPRESS;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
