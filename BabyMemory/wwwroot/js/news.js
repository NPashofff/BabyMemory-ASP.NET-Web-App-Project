// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
'use strict';
// news api key: 262304592d2741dbabf80cd6efe6dbe1
const content = $('#news-wraper');
const API_KEY = '262304592d2741dbabf80cd6efe6dbe1';
const API_URL = 'https://newsapi.org/v2/top-headlines';

function getNews() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `${API_URL}?country=us&apiKey=${API_KEY}`,
            success: (news) => {
                resolve(news);
            },
            error: (error) => {
                reject(error);
            }
        });
    });
}

getNews()
    .then((news) => {
        const articles = news.articles;
        for (let i = 0; i < articles.length; i++) {
            const article = articles[i];
            const articleEl = document.createElement('article');
            articleEl.innerHTML = `

        <h2>${article.title}</h2>
        <p>${article.description}</p>
        <a href="${article.url}" target="_blank">Read more</a>
      `;
            const articleCard = document.createElement('div');
            //articleCard.className = "card col-12 col-sm-6 col-md-4 mb-2 d-flex align-items-stretch flex-column";
            //articleCard.style = "width: 18rem;";
            articleCard.innerHTML = `
              <div class="row">
    <div class="col-md-12">
        <div class="timeline">

            <div>
                <div class="card-body ">
                    <div class="row mt-2">
                        <div class="col-5">
                            <img src="${article.urlToImage}" alt="user-avatar" class=" img-fluid">
                        </div>
                        <div class="col-7">
                            <h1 class="lead"><b>${article.title}</b></h1>
                            <p class="text-muted text-sm"><b>${article.description}</p>
                            <a href="${article.url}" class="btn btn-light">Read more</a>
                        </div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </div>
    </div>
</div>  
`;
            content.append(articleCard);
        }
    })
    .catch((error) => {
        console.log(error);
    });

//<div class="row">
//    <div class="col-5">
//    <img class="img" src="${article.urlToImage}">
//    </div>
//    <div class="col-5">
//    <h3 class="timeline-header"><b>${article.title}</b></h3>

//    <div class="timeline-body">
//    <p>${article.description}</p>
//    </div>
//    <div class="timeline-footer">
//    
//    </div>
//    </div>

//    </div>