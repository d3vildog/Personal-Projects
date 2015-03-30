(function () {
    var snake = game.createSnake();
    var snakeDiv = document.createElement('div'),
        playground = document.getElementById('playground'),
        x = getNumber(snakeDiv.style.top),
        y = getNumber(snakeDiv.style.left),
        randomX = Math.floor((Math.random() * 520) + 1),
        randomY = Math.floor((Math.random() * 500));

    while(randomX % 20 != 0){
        randomX =  Math.floor((Math.random() * 520) + 1);
    }

    while(randomY % 20 != 0){
        randomY =  Math.floor((Math.random() * 500));
    }

    var apple = game.createApple(randomX, randomY);

    drawApple(apple);
    snakeDrawBody(snake.getLength() , snake);
    var snakeHead = document.getElementById('0');
    // Move snake Down
    document.addEventListener('keydown', function(ev){
        if(ev.keyCode === 40 && snake.getDirection() != 0){
            snake.changeDirection(2);
            snakeDiv.style.top = snake.getY();
            snakeHead.style.transform = 'rotate('+ 0 +'deg)';
        }
    });

    // Move snake Up
    document.addEventListener('keydown', function(ev){
        if(ev.keyCode === 38 && snake.getDirection() != 2){
            snake.changeDirection(0);
            snakeDiv.style.top = snake.getY();
            snakeHead.style.transform = 'rotate('+ 180 +'deg)';
        }
    });

    // Move snake Left
    document.addEventListener('keydown', function(ev){
        if(ev.keyCode === 37 && snake.getDirection() != 1){
            snake.changeDirection(3);
            snakeDiv.style.left = snake.getX();
            snakeHead.style.transform = 'rotate('+ 90 +'deg)';
        }
    });

    // Move snake Right
    document.addEventListener('keydown', function(ev){
        if(ev.keyCode === 39 && snake.getDirection() != 3){
            snake.changeDirection(1);
            snakeDiv.style.left = snake.getX();
            snakeHead.style.transform = 'rotate('+ 270 +'deg)';
        }
    });

    // Move snake without stopping
    var movement = setInterval(function(){
        if(snake.newElement == false){
            snake.move();
        }
        var snakeHead = snake.getElements()[0];

        var counter = 0;
        snake.getElements().forEach(function (element){
            if(counter != 0 &&
                snake.newElement == false &&
                snakeHead.getX() == element.getX() &&
                snakeHead.getY() == element.getY()){
                showGameOver(snake);
                clearInterval(movement);
                return;
            }
            counter++;
        });

        if((snakeHead.getX() === apple.getX()) &&
            (snakeHead.getY() === apple.getY())){
            randomX = Math.floor((Math.random() * 520) + 1);
            randomY = Math.floor((Math.random() * 500));
            while(randomX % 20 != 0){
                randomX =  Math.floor((Math.random() * 520) + 1);
            }

            while(randomY % 20 != 0){
                randomY =  Math.floor((Math.random() * 500));
            }

            snake.points += 65;
            var points = document.getElementById('poi');
            poi.innerHTML = snake.points;
            snake.addElement();
            snake.move();
            var divv = document.createElement('div');
            divv.setAttribute('class', 'snakeBody');
            divv.setAttribute('id', snake.getElements().length - 1);
            divv.style.top = snake.getElements()[snake.getElements().length - 1].getY();
            divv.style.left = snake.getElements()[snake.getElements().length - 1].getX();
            playground.appendChild(divv);
            playground.removeChild(document.getElementById('apple'));
            apple = game.createApple(randomX, randomY);
            drawApple(apple);
        }

        if(!isInPlaygroundBoundries(getNumber(snake.getX()), getNumber(snake.getY()))){
            showGameOver(snake);
            clearInterval(movement);
            return
        }

        changeSnakePosition(snake.getElements().length, snake, snakeDiv);
    }, 100);
}());

function getNumber(str){
    var number = str.substring(0, str.length - 2);

    return parseInt(number);
}

function isInPlaygroundBoundries(snakeX, snakeY){
    if(snakeX < 0 || snakeX > 500 + 20){
        return false;
    }
    if(snakeY < 0 || snakeY > 500 - 20){
        return false;
    }
    return true;
}

function snakeDrawBody(snakeLength, snake){
    var playground = document.getElementById('playground'),
        newDiv;

    for(var i = 0; i < snakeLength; i++){
        newDiv = document.createElement('div');
        if(i == 0){
            newDiv.setAttribute('class', 'snake');
        }else {
            newDiv.setAttribute('class', 'snakeBody');
        }

        newDiv.setAttribute('id', i);
        newDiv.style.top = snake.getElements()[i].getY();
        newDiv.style.left = snake.getElements()[i].getX();
        playground.appendChild(newDiv);
    }
}

function changeSnakePosition(snakeLength, snake, snakeDiv){
    for(var i = 0; i < snakeLength; i++){
        var a = document.getElementById(i);
        a.style.top = snake.getElements()[i].getY();
        a.style.left = snake.getElements()[i].getX();
    }
}

function drawApple(apple){
    var appleDiv = document.createElement('div');
    var playground = document.getElementById('playground');
    appleDiv.setAttribute('id', 'apple');

    appleDiv.style.top = apple.getY();
    appleDiv.style.left = apple.getX();
    playground.appendChild(appleDiv);
}

function showGameOver(snake){
    var gameover = document.createElement('div');
    var gamoverh2 = document.createElement('h2');
    gamoverh2.innerHTML = "Game Over!";
    var totalscore = document.createElement('div');
    totalscore.setAttribute('id', 'totalScore');
    totalscore.innerHTML = 'Total score: ' + snake.points;
    gameover.setAttribute('id', 'gameover');
    gameover.appendChild(gamoverh2);
    gameover.appendChild(totalscore);
    var playground = document.getElementById('playground').appendChild(gameover);
}