# Steam_Review_api
스팀 리뷰 내용 API 저장소 입니다.

### 테이블
```sql
create table game_review_data(
    review_id int not null,
    game varchar(100) not null,
    year INT NOT NULL,
    review TEXT NOT NULL,
    sentiment VARCHAR(20) NOT NULL,
    language VARCHAR(10) NOT NULL,
    INDEX idx_game_year (game, year),
    INDEX idx_sentiment (sentiment),
    INDEX idx_language (language)
)
```
