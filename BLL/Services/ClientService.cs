using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using System.Net;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Client> _clientRepository;

        public ClientService(IMapper _mapper, IRepository<Client> _clientRepository)
        {
            this._mapper = _mapper;
            this._clientRepository = _clientRepository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAll();
                return _mapper.Map<IEnumerable<ClientDto>>(clients);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting clients", ex);
            }
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var client = await _clientRepository.GetById(id);

                if (client == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<ClientDto>(client);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting client by id", ex);
            }
        }

        public async Task AddClientAsync(ClientDto client)
        {
            try
            {
                var clientToInsert = _mapper.Map<Client>(client);
                _clientRepository.Insert(clientToInsert);
                await _clientRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding client", ex);
            }
        }

        public async Task UpdateClientAsync(ClientDto client)
        {
            try
            {
                var clientToInsert = _mapper.Map<Client>(client);
                _clientRepository.Update(clientToInsert);
                await _clientRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating client", ex);
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            if (await GetClientByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _clientRepository.Delete(id);
                await _clientRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting client", ex);
            }
        }
    }
}
