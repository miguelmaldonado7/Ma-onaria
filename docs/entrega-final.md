# Preparação para 16/11

A nova cena Catedral é gerada sem substituir a cena Claustro. Em um projeto já aberto, usar Maçonaria > Preparar cena inicial. Os prefabs são compartilhados: manter cópia da versão anterior antes de importar.

Mudanças implementadas: arena de 76 metros com alas de colunas, sepulturas, velas, jardim e santuário; textura de pedra repetida em escala maior; efeitos reduzidos; detalhes de vestes e animação procedural dos braços; três armas com cinco níveis independentes e escolha por teclas 1, 2 e 3 ao evoluir. A escolha pausa a partida. Relicário dispara projéteis, incensário atinge uma área e lâminas causam dano em posições orbitais.

Revisão de textura: a versão antiga preservava cenas existentes, o que pode manter materiais de uma geração anterior. O shader continua Standard, para Built-in. Não há evidência suficiente para atribuir a falha observada ao Linux. Conferir textura de pedra no material e renderização da cena Catedral no editor e no executável.

Pendências: execução e compilação no Unity; perfil de desempenho; teste de todas as combinações de armas; acabamento dos modelos além das formas geométricas; animações de ataque, dano e morte; melhoria visual do incensário. A movimentação inimiga ainda é perseguição direta e deve ser testada ao redor de obstáculos. Não há assets externos adicionados.

A revisão de código e o diff não substituem uma partida completa. Esta versão ainda não foi validada para apresentação.
