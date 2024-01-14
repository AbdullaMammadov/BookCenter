document.getElementById('burger').addEventListener('change', function () {
    var phoneContainer = document.querySelector('.phone-container');
    phoneContainer.style.display = this.checked ? 'block' : 'none';
    var SearchContainer = document.querySelector('.header-search');
    SearchContainer.style.display = this.checked ? 'none' : 'block'

});

document.querySelector('.p-mehsul').addEventListener('click', function () {
  var mehDownElement = document.querySelector('.meh-down');
 
  mehDownElement.classList.toggle('tr-270');
});
document.getElementById("mehsul").addEventListener("click", function(event) {
  event.preventDefault();
  console.log("Element clicked, default behavior prevented.");
});


document.querySelector('.book-head').addEventListener('click', function () {
  var mehDownElement = document.querySelector('.book-cat');
  document.querySelector('.book-head').classList.toggle('color-black')
  mehDownElement.classList.toggle('d-block');
});




const mehsulList = document.querySelectorAll('.mehsul');
const mehCatList = document.querySelectorAll('.meh-cat');


mehsulList.forEach(function (mehsul, index) {
  mehsul.addEventListener('mouseover', function () {
    mehCatList[index].style.display = 'block';
  });

  mehsul.addEventListener('mouseout', function () {
    mehCatList[index].style.display = 'none';
  });

});

mehCatList.forEach(function (mehsul, index) {
    mehsul.addEventListener('mouseover', function () {
      mehCatList[index].style.display = 'block';
    });
  
    mehsul.addEventListener('mouseout', function () {
      mehCatList[index].style.display = 'none';
    });
  
  });
  document.addEventListener('DOMContentLoaded', function () {
    const pMehsul = document.querySelector('.p-mehsul');
    const pMehCat = document.querySelector('.p-meh-cat');
  
    pMehsul.addEventListener('click', function () {
      pMehCat.style.display = (pMehCat.style.display === 'block') ? 'none' : 'block';
    });
  });
  
  document.getElementById('searchbox').addEventListener('focus', function() {
  
    document.querySelector('.search').classList.add('d-block');
  });
  
  document.getElementById('searchbox').addEventListener('blur', function() {
  
    document.querySelector('.search').classList.remove('d-block');
  });

  function showDetails(bookId) {
    var detailElement = document.getElementById("detail-" + bookId);
    var detailBlackElement = document.getElementById("black-" + bookId);
    var body=document.querySelector("body")
    body.classList.toggle("overflow-hidden")
    detailElement.classList.toggle("d-block");
    detailBlackElement.classList.toggle("d-block");
  }
  
  document.querySelectorAll('.detail-black').forEach(function (detailBlackElement) {
    detailBlackElement.addEventListener('click', function () {
      var detailId = this.id.replace('black-', ''); 
      var detailElement = document.getElementById('detail-' + detailId);
      var body=document.querySelector("body")
      body.classList.toggle("overflow-hidden")
      detailElement.classList.toggle('d-block', false);
      detailBlackElement.classList.toggle('d-block', false);
    });
  });
$(document).ready(function () {
    $(document).on("keyup", "#serachbox", function () {
        $("#searchbox").slice(0).remove();
        let value = $(this).val();
        $.ajax({
            method: "get",
            url: `/product/searchproduct?text=${value}`,
            success: function (data) {
                $(".search").append(data);
            }

        })
    });



});
