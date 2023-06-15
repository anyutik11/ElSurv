class Review {
  final String id;
  final bool deleted;
  final String description;
  final String question1;
  final String question2;
  final String question3;

  const Review(
      {required this.id,
      required this.deleted,
      required this.description,
      required this.question1,
      required this.question2,
      required this.question3});

  factory Review.fromJson(Map<String, dynamic> json) {
    return Review(
      id: json['id'],
      deleted: json['deleted'],
      description: json['description'],
      question1: json['question1'],
      question2: json['question2'],
      question3: json['question3'],
    );
  }
}
