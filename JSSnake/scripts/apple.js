var game = game || {};

(function(scope){
    function Apple(x, y){
        this.x = x || 0;
        this.y = y || 0;
    }

    Apple.prototype.getX = function getX(){
        return this.x;
    }

    Apple.prototype.getY = function getY(){
        return this.y;
    }

    scope.createApple = function createApple(x, y){
        return new Apple(x, y);
    }
}(game));
