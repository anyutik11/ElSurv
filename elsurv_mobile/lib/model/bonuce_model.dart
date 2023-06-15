class Bonuce {
  final String id;
  final String dtd;
  final int sum;
  final String remark;

  const Bonuce({
    required this.id,
    required this.dtd,
    required this.sum,
    required this.remark,
  });

  factory Bonuce.fromJson(Map<String, dynamic> json) {
    return Bonuce(
      id: json['id'],
      dtd: json['dtd']?.length > 16
          ? json['dtd'].toString().substring(0, 10)
          : '',
      sum: json['sum'],
      remark: json['remark'],
    );
  }
}
