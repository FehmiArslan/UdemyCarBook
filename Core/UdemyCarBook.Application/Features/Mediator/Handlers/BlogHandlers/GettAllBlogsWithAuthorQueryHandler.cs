using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces.BlogInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GettAllBlogsWithAuthorQueryHandler : IRequestHandler<GettAllBlogsWithAuthorQuery, List<GettAllBlogsWithAuthorQueryResult>>
    {
        private readonly IBlogRepository _repository;

        public GettAllBlogsWithAuthorQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GettAllBlogsWithAuthorQueryResult>> Handle(GettAllBlogsWithAuthorQuery request, CancellationToken cancellationToken)
        {
                var values = _repository.GetAllBlogsWithAuthors();
                return values.Select(x => new GettAllBlogsWithAuthorQueryResult
                {
                    AuthorID = x.AuthorID,
                    BlogID = x.BlogID,
                    CategoryID = x.CategoryID,
                    CoverImageUrl = x.CoverImageUrl,
                    CreatedDate = x.CreatedDate,
                    Title = x.Title,
                    AuthorName = x.Author.Name,
                    Description= x.Description,
                    AuthorDescription=x.Author.Description,
                    AuthorImageUrl=x.Author.ImageUrl
            
                }).ToList();
        }
    }

}
