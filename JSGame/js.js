var body = document.getElementById('wrapper');
var i = 0;

//setInterval(function(){
//    i+=1;
//    body.style.backgroundPosition = i + "px " + "0px";
//}, 10);

var sonic = document.getElementById('sonic');
var a = 0;
var left = 50;

document.addEventListener('keydown', function(ev){
  if(ev.keyCode == 39){
      sonic.style.transform = "scaleX(1)";
      setTimeout(function(){
          sonic.style.backgroundPositionX = a + "px";
          left+=10;
          document.getElementById('sonic').style.left = left + "px";
          a += 200;
          if(a >= 800){
              a = 0;
          }
      }, 100);
      i+=1;
      body.style.backgroundPosition = i + "px " + "0px";
  }
});

document.addEventListener('keyup', function(ev){
    if(ev.keyCode == 39){
        a = -400;
    }
});

document.addEventListener('keydown', function(ev){
    if(ev.keyCode == 37){
        sonic.style.transform = "scaleX(-1)";
        setTimeout(function(){
            sonic.style.backgroundPositionX = a + "px";
            left-=10;
            document.getElementById('sonic').style.left = left + "px";
            a += 200;
            if(a >= 800){
                a = 0;
            }
        }, 100);
        i-=1;
        body.style.backgroundPosition = i + "px " + "0px";
    }
});

document.addEventListener('keyup', function(ev){
    if(ev.keyCode == 37){
        a = -400;
    }
});