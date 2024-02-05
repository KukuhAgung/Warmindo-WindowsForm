using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warmindo
{
    public class Pesanan
    {
        private int noPesanan;
        private string nama;
        private int total;
        private string status;
        public int NoPesanan { get => noPesanan; set => noPesanan = value; }
        public string Nama { get => nama; set => nama = value; }
        public int Total { get => total; set => total = value; }
        public string Status { get => status; set => status = value; }

        public void Pesan(int getnopesanan, string getnama, int gettotal, string getstatus)
        {
            this.noPesanan= getnopesanan;
            this.nama = getnama;
            this.total = gettotal;
            this.status = getstatus;
        }
    }
}