using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThiThuNet103.Models;

namespace ThiThuNet103.Controllers
{
    public class CaController : Controller
    {
        FinalNet103Context context = new FinalNet103Context();
        public CaController()
        {
            context = new FinalNet103Context();
        }
        // GET: CaController
        public ActionResult Index()
        {
            var listCa = from ca in context.Cas
                         join dongvat in context.Dongvats
                         on ca.Idca equals dongvat.Id
                         select new CaViewModel
                         {
                             Id = ca.Id,
                             Ten = ca.Ten,
                             ThucAn = ca.ThucAn,
                             TapTinh = ca.TapTinh,
                             NoiSong = dongvat.Noisong,
                             TuoiThoTB = (Int32)dongvat.TuoithoTb
                         };
            return View(listCa);
        }

        // GET: CaController/Details/5
        public ActionResult Details(int id)
        {
            var detail = context.Cas.Find(id);
            return View(detail);
        }

        // GET: CaController/Create
        public ActionResult Create()
        {
            List<Dongvat> dongvat = context.Dongvats.ToList();
            ViewBag.Iddongvat = dongvat;
            return View();
        }

        // POST: CaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ca ca)
        {
            try
            {
                context.Cas.Add(ca);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        // GET: CaController/Edit/5
        public ActionResult Edit(int id)
        {
            List<Dongvat> dongvat = context.Dongvats.ToList();
            ViewBag.Iddongvat = dongvat;
            var itemEdit = context.Cas.Find(id);
            return View(itemEdit);

            //var itemEdit = context.Cas.FirstOrDefault(x=>x.Id==id);
            //try
            //{
            //    itemEdit.Ten = ca.Ten;
            //    itemEdit.ThucAn = ca.ThucAn;
            //    itemEdit.TapTinh = ca.TapTinh;
            //    context.Update(itemEdit);
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch (Exception e )
            //{
            //    return Content(e.Message);
            //}

        }

        // POST: CaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ca ca, int id)
        {
            var itemEdit = context.Cas.FirstOrDefault(x => x.Id == id);
            List<Ca> EditList = new List<Ca>();
            var EditValue = HttpContext.Session.GetString("edit");
            if (string.IsNullOrEmpty(EditValue))
            {
                EditList.Add(itemEdit);
            }
            else
            {
                EditList = JsonConvert.DeserializeObject<List<Ca>>(EditValue);
                EditList.Add(itemEdit);
            }
            string json = JsonConvert.SerializeObject(EditList);
            HttpContext.Session.SetString("edit", json);

            try
            {
                itemEdit.Ten = ca.Ten;
                itemEdit.ThucAn = ca.ThucAn;
                itemEdit.TapTinh = ca.TapTinh;
                context.Update(itemEdit);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        // GET: CaController/Delete/5
        public ActionResult Delete(int id)
        {
            List<Ca> deletedList = new List<Ca>();
            var deletedValue = HttpContext.Session.GetString("deleted");
            var deletedItem = context.Cas.Find(id);
            if (string.IsNullOrEmpty(deletedValue))
            {
                deletedList.Add(deletedItem);
            }
            else
            {
                deletedList = JsonConvert.DeserializeObject<List<Ca>>(deletedValue);
                deletedList.Add(deletedItem);
            }
            string jsonData = JsonConvert.SerializeObject(deletedList);
            HttpContext.Session.Remove("deleted");
            HttpContext.Session.SetString("deleted", jsonData);
            context.Remove(deletedItem);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: CaController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public ActionResult RollBackDel()
        {
            var sessionData = HttpContext.Session.GetString("deleted");

            if (string.IsNullOrEmpty(sessionData))
            {
                return Content("Không có đối tượng nào bị xóa");
            }
            else
            {
                List<Ca> ca = JsonConvert.DeserializeObject<List<Ca>>(sessionData);
                foreach (var item in ca)
                {
                    Ca newca = new Ca()
                    {
                        Ten = item.Ten,
                        ThucAn = item.ThucAn,
                        TapTinh = item.TapTinh,
                    };
                    context.Cas.Add(newca);
                }
                context.SaveChanges();
                HttpContext.Session.Remove("deleted");
                return RedirectToAction("Index");
            }
        }
        public ActionResult RollBackEdit()
        {
            var sessionData = HttpContext.Session.GetString("edit");

            if (string.IsNullOrEmpty(sessionData))
            {
                return Content("Không có đối tượng nào bị sửa");
            }
            else
            {
                List<Ca> ca = JsonConvert.DeserializeObject<List<Ca>>(sessionData);
                foreach (var item in ca)
                {
                    var rolledit = context.Cas.Find(item.Id);
                    rolledit.Ten = item.Ten;
                    rolledit.ThucAn = item.ThucAn;
                    rolledit.TapTinh = item.TapTinh;
                }
                context.SaveChanges();
                HttpContext.Session.Remove("edit");
                return RedirectToAction("Index");
            }
        }
    }
}
