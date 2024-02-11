using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.ReservationnCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.ReservationnHandlers
{
    public class CreateReservationnCommandHandler : IRequestHandler<CreateReservationnCommand>
    {
        private readonly IRepository<Reservationn> _repository;

        public CreateReservationnCommandHandler(IRepository<Reservationn> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateReservationnCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Reservationn
            {
                Age = request.Age,
                CarID = request.CarID,
                Description = request.Description,
                DriverLicenseYear = request.DriverLicenseYear,
                DropOffLocationID = request.DropOffLocationID,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                PickUpLocationID = request.PickUpLocationID,
                Surname = request.Surname,
                Status = "Rezervasyon alındı"
            });
        }
    }
}
