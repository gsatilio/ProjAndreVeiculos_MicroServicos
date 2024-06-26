﻿using Models;
using Services;

namespace Controllers
{
    public class ConductorController
    {
        private ConductorService _service = new();

        public ConductorController()
        {

        }
        public async Task<string> Insert(Conductor conductor, int type)
        {
            string result = null;
            try
            {
                result = await _service.Insert(conductor, type);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<List<Conductor>> GetAll(int type)
        {
            List<Conductor> list = new List<Conductor>();
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
        public async Task<Conductor> Get(string document, int type)
        {
            Conductor list = new Conductor();
            try
            {
                list = await _service.Get(document, type);
            }
            catch
            {
                throw;
            }
            return list;
        }
    }
}
