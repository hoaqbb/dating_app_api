﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Helpers;
using datingapp_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace datingapp_api.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatingAppContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DatingAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages
                .Include(u => u.Sender)
                .Include(u => u.Recipient)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.Username == messageParams.Username 
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.Username == messageParams.Username
                    && u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.Username == messageParams.Username 
                    && u.RecipientDeleted == false 
                    && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.Recipient.Username == currentUsername && m.RecipientDeleted == false
                    && m.Sender.Username == recipientUsername
                    || m.Recipient.Username == recipientUsername
                    && m.Sender.Username == currentUsername && m.SenderDeleted == false)
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Recipient.Username == currentUsername).ToList();

            if(unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
