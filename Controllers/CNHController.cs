﻿using Models;
using Services;

namespace Controllers
{
    public class CNHController
    {
        private CNHService _service = new();

        public CNHController()
        {

        }
        public int Insert(CNH cnh, int type)
        {
            int result = 0;
            try
            {
                result = _service.Insert(cnh, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<CNH>> GetAll(int type)
        {
            List<CNH> list = new List<CNH>();
            try
            {
                list = await _service.GetAll(type);
            }
            catch
            {
                throw;
            }
            return list;
        }
        public async Task<CNH> Get(int id, int type)
        {
            CNH list = new CNH();
            try
            {
                list = await _service.Get(id, type);
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}
