using ASSIGNMENT.Context;
using ASSIGNMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ASSIGNMENT
{
    public partial class ServiceDanhBa : ISeviceDanhBa
    {
        DatabaseContext databaseContext;
        List<Nguoi> _lstnguoi;
        List<DanhBa> _lstdanhba;
        public ServiceDanhBa()
        {
            databaseContext = new DatabaseContext();
            _lstnguoi = new List<Nguoi>();
            _lstdanhba = new List<DanhBa>();
            getlistfromdb();
        }
        public void getlistfromdb()
        {
            _lstnguoi= databaseContext.Nguois.ToList();
            _lstdanhba = databaseContext.DanhBas.ToList();
        }
        public List<Nguoi> getlstnguoi()
        {
            try
            {
                return _lstnguoi;
            }
            catch (Exception)
            {

                return null;
            }
            
        }
        public List<DanhBa> getlstdanhba()
        {
            
            try
            {
                return _lstdanhba;
            }
            catch (Exception)
            {

                return null;
            }
        }
       
        public string Add(Nguoi nguoi,DanhBa danhBa)
        {
            try
            {
                nguoi.Id = _lstnguoi.Count;
                databaseContext.Nguois.Add(nguoi);
                danhBa.Id = _lstdanhba.Count;
                danhBa.IdNguoi = nguoi.Id;
                databaseContext.DanhBas.Add(danhBa);
                databaseContext.SaveChanges();
                return "them thanh cong";
            }
            catch (Exception e)
            {

                return "them that bai" + e.Message;
            }
          
        }
        public string Del(int idnguoi)
        {
            try
            {
                var id1 = _lstdanhba.Where(c => c.IdNguoi == idnguoi).FirstOrDefault();
                var id = _lstnguoi.Where(c => c.Id == idnguoi).FirstOrDefault();

                databaseContext.DanhBas.Remove(id1);
                databaseContext.Nguois.Remove(id);
                databaseContext.SaveChanges();
                return "xoa thanh cong";
            }
            catch (Exception e)
            {

                return "xoa that bai" + e.Message;
            }
           




           
        }
        public string update(DanhBa iddanhba,Nguoi idnguoi)
        {
            //var id1 = databaseContext.DanhBas.FirstOrDefault(c => c.Id == iddanhba);
            //var id = databaseContext.Nguois.FirstOrDefault(c => c.Id == idnguoi);
            try
            {
                databaseContext.DanhBas.Update(iddanhba);
                databaseContext.Nguois.Update(idnguoi);
                databaseContext.SaveChanges();

                return "sua thanh cong";
            }
            catch (Exception e)
            {

                return "update that bai" + e.Message;
            }
           
        }
        public string[] getYearOfBirth()
        {
            string[] arrYears = new string[2021 - 1900];
            int temp = 1900;
            for (int i = 0; i < arrYears.Length; i++)
            {
                arrYears[i] = temp.ToString();
                temp++;
            }

            return arrYears;
        }
       
        public List<Nguoi> Search(string db)
        {
            return _lstnguoi.Where(c => c.Ten.StartsWith(db)).ToList();
        }
        public List<DanhBa> Search1(string db)
        {
            return _lstdanhba.Where(c => c.Sdt1.StartsWith(db)|| c.Sdt2.StartsWith(db)).ToList();
        }
        public List<Nguoi> LocTBCC()
        {
            return _lstnguoi.OrderBy(c => c.Ho).ToList();
        }
        public List<Nguoi> LocTTen(string ten)
        {
            

            
            return _lstnguoi.OrderBy(c=>c.Ten).ToList();
        }
        public List<Nguoi> LocTGT()
        {
            
            return _lstnguoi.Where(c => c.GioiTinh==true).ToList();
        }
        public List<Nguoi> LocTGT1()
        {

            return _lstnguoi.Where(c => c.GioiTinh == false).ToList();
        }
        public List<DanhBa> NhamangViettel()
        {
            return _lstdanhba.Where(c => c.Sdt1.Contains("096") || c.Sdt1.Contains("097") || c.Sdt1.Contains("098") || c.Sdt1.Contains("086") ||
            c.Sdt1.Contains("032") || c.Sdt1.Contains("033") || c.Sdt1.Contains("034") || c.Sdt1.Contains("035") || c.Sdt1.Contains("036")
            || c.Sdt1.Contains("037") || c.Sdt1.Contains("038") || c.Sdt1.Contains("039") || c.Sdt2.Contains("096") || c.Sdt2.Contains("097") || c.Sdt2.Contains("098") || c.Sdt2.Contains("086") ||
            c.Sdt2.Contains("032") || c.Sdt2.Contains("033") || c.Sdt2.Contains("034") || c.Sdt2.Contains("035") || c.Sdt2.Contains("036")
            || c.Sdt2.Contains("037") || c.Sdt2.Contains("038") || c.Sdt2.Contains("039")).ToList();
        }
        public List<DanhBa> NhamangVina()
        {
            return _lstdanhba.Where(c => c.Sdt1.Contains("088") || c.Sdt1.Contains("091") || c.Sdt1.Contains("094") || c.Sdt1.Contains("081") ||
            c.Sdt1.Contains("082") || c.Sdt1.Contains("083") || c.Sdt1.Contains("084") || c.Sdt1.Contains("085") || c.Sdt2.Contains("088") || c.Sdt2.Contains("091") || c.Sdt2.Contains("094") || c.Sdt1.Contains("081") ||
            c.Sdt2.Contains("082") || c.Sdt2.Contains("083") || c.Sdt2.Contains("084") || c.Sdt2.Contains("085")).ToList();
        }
    }
}
