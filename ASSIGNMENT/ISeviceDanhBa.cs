using ASSIGNMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASSIGNMENT
{
    partial interface ISeviceDanhBa
    {
        public void getlistfromdb();


        public List<Nguoi> getlstnguoi();

        public List<DanhBa> getlstdanhba();

        public string Add(Nguoi nguoi, DanhBa danhBa);

        public string Del(int idnguoi);

        public string update(DanhBa iddanhba, Nguoi idnguoi);

        public string[] getYearOfBirth();

        public List<Nguoi> Search(string db);

        public List<DanhBa> Search1(string db);

        public List<Nguoi> LocTBCC();

        public List<Nguoi> LocTTen(string ten);

        public List<Nguoi> LocTGT();

        public List<Nguoi> LocTGT1();

        public List<DanhBa> NhamangViettel();

        public List<DanhBa> NhamangVina();

       
        
    }
}
