using AuthPolicyService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPolicyService.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Root> items = new List<Root>();
            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json")))
            {
                string json = r.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(json))
                {
                    items = JsonConvert.DeserializeObject<List<Root>>(json);
                }
            }
            return View(items);
        }

        public IActionResult Create()
        {
            RootCreate policyCreate = new RootCreate()
            {
                Policies = new Policy
                {
                    Parameters = new List<ClaimModel>()
                }
            };
            ViewBag.Operators = new List<SelectListItem> {
                new SelectListItem
                {
                    Text = "equals to",
                    Value = "="
                },
                new SelectListItem
                {
                    Text = "Not Equals to",
                    Value = "!="
                },
                new SelectListItem
                {
                    Text = "Greater Than",
                    Value = ">"
                },
                new SelectListItem
                {
                    Text = "Less Than",
                    Value = "<"
                },
                new SelectListItem
                {
                    Text = "Less Than Equal To",
                    Value = "<="
                },
                new SelectListItem
                {
                    Text = "Greater Than Equal To",
                    Value = ">="
                }
            };
            return View(policyCreate);
        }

        [HttpPost()]
        public bool InsertPolicyInJson([FromBody]Root policy)
        {
            List<Root> items = new List<Root>();
            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json")))
            {
                string json = r.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(json))
                {
                    //Read file Json
                    items = JsonConvert.DeserializeObject<List<Root>>(json);
                    //Update object in Json

                    var isUrlExist = items.Any(x => x.Url.ToLower().Equals(policy.Url.ToLower()));
                    if (isUrlExist)
                    {
                        var isPolicyExist = items.Any(x => x.Policies.Any(y => y.Name.ToLower().Equals(policy.Policies.FirstOrDefault().Name.ToLower())));
                        if (isPolicyExist)
                        {
                            //Log policy name already exist please use edit option
                            return false;
                        }
                        else
                        {
                            //Get existing policy list
                            var existingPolicyList = items.Where(x => x.Url.ToLower().Equals(policy.Url.ToLower())).Select(x => x.Policies).FirstOrDefault();
                            //Adding new policy to list
                            existingPolicyList.Add(policy.Policies.FirstOrDefault());
                            //Replacing modified existing policy list
                            items.Where(x => x.Url.ToLower().Equals(policy.Url.ToLower())).FirstOrDefault().Policies = existingPolicyList;

                            //Updating other parameters
                            items.FirstOrDefault().PolicyOperator = policy.PolicyOperator;
                            items.FirstOrDefault().PolicyInfo = policy.PolicyInfo;
                        }
                    }
                    else
                    {
                        items.Add(policy);
                    }
                }
                else
                {
                    //Add new Entry in file
                    items.Add(policy);
                }
            }
            string jsonStr = JsonConvert.SerializeObject(items, Formatting.Indented);
            System.IO.File.WriteAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"), jsonStr);
            return true;
        }


        public IActionResult Edit(string id)
        {
            Root model = new Root();
            List<Root> items = null;
            var jsonString = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"));


            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json")))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Root>>(json);
            }
            var result = items.Where(x => x.SrNo == id).FirstOrDefault();

            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(Root root)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Doctors.Add(doctor);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(root);
        }

        public IActionResult Details(string id)
        {

            Root model = new Root();
            List<Root> items = null;
            var jsonString = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"));


            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json")))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Root>>(json);
            }
            var result = items.Where(x => x.SrNo == id).FirstOrDefault();

            return View(result);

        }
        public IActionResult Delete(string id)
        {
            Root model = new Root();
            List<Root> items = null;
            var jsonString = System.IO.File.ReadAllText(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json"));


            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Policies.json")))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Root>>(json);
            }
            var result = items.Where(x => x.SrNo == id).FirstOrDefault();

            return View(result);
        }
    }
}