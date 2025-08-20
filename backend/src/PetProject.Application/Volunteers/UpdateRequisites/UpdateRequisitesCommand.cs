using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(Guid Id, IEnumerable<RequisitesDto> Requisites);