﻿using BusinessLogic.Applications.Interfaces;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemApplication _itemApplication;
        public ItemsController(IItemApplication itemAplication)
        {
            _itemApplication = itemAplication;
        }
        // GET: api/Items
        public HttpResponseMessage Get()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data Available");
            List<Item> get = _itemApplication.Get();
            if (get.Count() != 0)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, get);
                return message;
            }
            return message;
        }

        // GET: api/Items/5
        public HttpResponseMessage Get(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Such Data Exist");
            var get = _itemApplication.Get(id);
            if (get != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, get);
                return message;
            }
            return message;
        }

        // POST: api/Items
        public HttpResponseMessage Post([FromBody]ItemVM itemVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _itemApplication.Insert(itemVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, itemVM);
                return message;
            }
            return message;
        }

        // PUT: api/Items/5
        public HttpResponseMessage Put(int id, [FromBody]ItemVM itemVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _itemApplication.Update(id, itemVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, itemVM);
                return message;
            }
            return message;
        }

        // DELETE: api/Items/5
        public HttpResponseMessage Delete(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Such Data Exist");
            var result = _itemApplication.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, "Data has succesfully deleted");
                return message;
            }
            return message;
        }
    }
}
