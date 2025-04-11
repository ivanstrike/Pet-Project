using PetProject.Application.DTO;
using PetProject.Domain.Shared.Value_Objects;

namespace PetProject.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(Guid Id, IEnumerable<RequisitesDto> Requisites);