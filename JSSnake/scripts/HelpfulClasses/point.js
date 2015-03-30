var game = game || {};

(function(scope){
    function Point(x, y){
        this.x = x;
        this.y = y;
    };

    Point.prototype.getX = function getX(){
        return this.x;
    };

    Point.prototype.setX = function setX(x){
        this.x = x;
    };

    Point.prototype.getY = function getY(){
        return this.y;
    }

    Point.prototype.setY = function setY(y){
        this.y = y;
    };

    scope.createPoint = function createPoint(x, y){
        return new Point(x, y);
    };
})(game);
