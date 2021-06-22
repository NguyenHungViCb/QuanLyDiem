const parentElements = document.getElementsByClassName('parent-element');
let parentElementsArr = Array.from(parentElements);

function toggleShow(p){
    p.classList.toggle("slideup")
    p.classList.toggle("slidedown")
}
function toggleHighlight(p){
    p.classList.toggle("highlight");
}

parentElementsArr.map(p => p.childNodes[1] 
    ? p.childNodes[1].addEventListener('click', e=> toggleShow(p.childNodes[3])) :"");
parentElementsArr.map(p => p.childNodes[1] 
    ? p.childNodes[1].addEventListener('click', e=> toggleHighlight(p.childNodes[1])):"")