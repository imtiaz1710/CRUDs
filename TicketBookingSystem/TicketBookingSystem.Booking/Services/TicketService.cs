using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Exceptions;
using TicketBookingSystem.Booking.UnitOfWorks;

namespace TicketBookingSystem.Booking.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;

        public TicketService(IBookingUnitOfWork bookingUnitOfWork)
        {
            _bookingUnitOfWork = bookingUnitOfWork;
        }

        public IList<Ticket> GetAllTickets()
        {
            var TicketEntities = _bookingUnitOfWork.Tickets.GetAll();
            var Tickets = new List<Ticket>();

            foreach (var entity in TicketEntities)
            {
                var Ticket = new Ticket()
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Destination = entity.Destination,
                    TicketFee = entity.TicketFee
                };

                Tickets.Add(Ticket);
            }

            return Tickets;
        }

        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var TicketData = _bookingUnitOfWork.Tickets.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Destination.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from Ticket in TicketData.data
                              select new Ticket
                              {
                                  Id = Ticket.Id,
                                  CustomerId = Ticket.CustomerId,
                                  Destination = Ticket.Destination,
                                  TicketFee = Ticket.TicketFee
                              }).ToList();
            return (resultData, TicketData.total, TicketData.totalDisplay);
        }

        public Ticket GetTicket(int id)
        {
            var Ticket = _bookingUnitOfWork.Tickets.GetById(id);

            if (Ticket == null) return null;

            return new Ticket
            {
                Id = Ticket.Id,
                CustomerId = Ticket.CustomerId,
                Destination = Ticket.Destination,
                TicketFee = Ticket.TicketFee
            };
        }

        public void CreateTicket(Ticket Ticket)
        {
            if (Ticket == null)
                throw new InvalidParameterException("Ticket was not provided");

            _bookingUnitOfWork.Tickets.Add(
                new Entities.Ticket
                {
                    CustomerId = Ticket.CustomerId,
                    Destination = Ticket.Destination,
                    TicketFee = Ticket.TicketFee
                }
            );

            _bookingUnitOfWork.Save();
        }

        public void UpdateTicket(Ticket Ticket)
        {
            if (Ticket != null)
                throw new InvalidParameterException("Ticket is not given");

            var TicketEntity = _bookingUnitOfWork.Tickets.GetById(Ticket.Id);

            if (TicketEntity != null)
            {
                TicketEntity.CustomerId = Ticket.CustomerId;
                TicketEntity.Destination = Ticket.Destination;
                TicketEntity.TicketFee = Ticket.TicketFee;
                _bookingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Ticket");

        }

        public void DeleteTicket(int id)
        {
            _bookingUnitOfWork.Tickets.Remove(id);
            _bookingUnitOfWork.Save();
        }
    }
}
