using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Dto;
using SignUp.Models;

namespace SignUp.Controllers
{
    public class BankDetailsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BankDetails
        
        [HttpGet]
        [Route("api/BankDetails/AllBankDetails")]
        public HttpResponseMessage AllBankDetails()
        {
            var bankDetailList = db.BankDetails.Select(bankDetail => new BankDetailListDto()

            {
                BankId = bankDetail.BankId,
                BankName = bankDetail.BankName,
                BankBranch = bankDetail.BankBranch,
                BankAccountNo = bankDetail.BankAccountNo,
                BankAccountType = bankDetail.BankAccountType,
                PaymentType = bankDetail.PaymentType,
                BankTransactionNo = bankDetail.BankTransactionNo,
                Status = bankDetail.Status
            });
            var bankDetailDisList = bankDetailList.OrderByDescending(b => b.BankId);

            return Request.CreateResponse(HttpStatusCode.OK, bankDetailDisList);
        }

        // GET: api/BankDetails/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult GetBankDetail(int id)
        {
            BankDetail bankDetail = db.BankDetails.Find(id);
            if (bankDetail == null)
            {
                return NotFound();
            }

            return Ok(bankDetail);
        }

        // PUT: api/BankDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBankDetail(int id, BankDetail bankDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankDetail.BankId)
            {
                return BadRequest();
            }

            db.Entry(bankDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BankDetails
        [HttpPost]
        [Route("api/BankDetails/CreateBankDetail")]
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult CreateBankDetail(BankDetail bankDetail)
        {
            try
            {
                db.BankDetails.Add(bankDetail);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri + bankDetail.BankId.ToString()), bankDetail);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex));
            }

        }
        [HttpPut]
        [Route("api/BankDetails/UpdateBankDetail/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateBankDetail([FromUri]int id, [FromBody]BankDetail bankDetail)
        {
            try
            {
                BankDetail bankDetailEntity = db.BankDetails.Find(id);
                if (bankDetailEntity == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "BankDetail with BankId= " + id.ToString() + " is not found"));

                }
                else
                {

                    bankDetailEntity.BankName = bankDetail.BankName;
                    bankDetailEntity.BankBranch = bankDetail.BankBranch;
                    bankDetailEntity.BankAccountNo = bankDetail.BankAccountNo;
                    bankDetailEntity.BankAccountType = bankDetail.BankAccountType;
                    bankDetailEntity.PaymentType = bankDetail.PaymentType;
                    bankDetailEntity.BankTransactionNo = bankDetail.BankTransactionNo;
                    bankDetailEntity.Status = bankDetail.Status;
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, bankDetailEntity));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        [HttpDelete]
        [Route("api/BankDetails/DeleteBankDetail/{id}")]
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult DeleteBankDetail(int id)
        {
            try
            {
                BankDetail bankDetail = db.BankDetails.Find(id);
                if (bankDetail == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Bank Detail with BankId= " + id.ToString() + " is not found"));
                }
                else
                {
                    db.BankDetails.Remove(bankDetail);
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "BankDetail Deleted"));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankDetailExists(int id)
        {
            return db.BankDetails.Count(e => e.BankId == id) > 0;
        }
    }
}