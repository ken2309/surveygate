// Get the currently focused element
const focusedElement = document.getElementsByClassName('search_input')[0];

focusedElement.addEventListener('focusin',function(){
  console.log('focusin');
  document.getElementsByClassName('search_wrapper')[0].classList.add('active');
})
focusedElement.addEventListener('focusout',function(){
  console.log('focusout');
  document.getElementsByClassName('search_wrapper')[0].classList.remove('active');
})