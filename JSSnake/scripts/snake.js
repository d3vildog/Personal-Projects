var game = game || {};

(function(scope){
	function Snake(length){
		this.x = 0;
		this.y = 0;
		this.length = length || 5;
        this.direction = 1;
        this.snakeElements = [];
        this.newElement = false;
        this.points = 0;
        this.lives = 2;
	};

    Snake.prototype.getX = function getX(){
        return this.x + 'px';
    };

    Snake.prototype.getY = function getY(){
        return this.y  + 'px';
    };

    Snake.prototype.getLength = function getLength(){
        return this.snakeElements.length;
    };

    Snake.prototype.changeDirection = function changeDirection(direction){
        this.direction = direction;
    };

    Snake.prototype.getDirection = function getDirection(){
        return this.direction;
    };

    Snake.prototype.getElements = function getElements(){
        return this.snakeElements;
    };

    Snake.prototype.addElement = function addElement(){
        this.newElement = true;
    };

	Snake.prototype.moveDown = function moveDown(){
		this.y+=20;
        this.direction = 2;
	};
	
	Snake.prototype.moveUp = function moveUp(){
		this.y-=20;
        this.direction = 0;
	};
	
	Snake.prototype.moveLeft = function moveLeft(){
		this.x-=20;
        this.direction = 3;
	};
	
	Snake.prototype.moveRight = function moveRight(){
		this.x+=20;
        this.direction = 1;
	};

    Snake.prototype.move = function move(){
        if(this.direction == 0){
            this.moveUp();
        }
        if(this.direction == 1){
            this.moveRight();
        }
        if(this.direction == 2){
            this.moveDown();
        }
        if(this.direction == 3){
            this.moveLeft();
        }
        this.moveElements(this.x, this.y);
    };

    Snake.prototype.inicializeBody = function inicializeBody(){
        this.snakeElements.push(scope.createPoint(this.x, this.y));

        for(var y = 1; y < this.length; y++){
            this.snakeElements.push(scope.createPoint(this.y, this.snakeElements[y - 1].getY() + 20));
        }
    };

    Snake.prototype.moveElements = function moveElements(x, y){
        var newSnakeElements = [];
        newSnakeElements.push(game.createPoint(x, y));

        for(var i = 1; i < this.snakeElements.length; i++){
            var newX = this.snakeElements[i - 1].getX();
            var newY = this.snakeElements[i - 1].getY();

            newSnakeElements.push(game.createPoint(newX, newY));
        }
        if(this.newElement){
            this.newElement = false;
            newSnakeElements.push(this.snakeElements[this.snakeElements.length - 1]);
        }
        this.snakeElements = newSnakeElements;
    }

	scope.createSnake = function createSnake(){
        var snake = new Snake();
        snake.inicializeBody();
		return snake;
	}
}(game));