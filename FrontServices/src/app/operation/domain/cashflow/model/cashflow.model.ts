import { DomainEntityBuilder } from "@app/shared/domain/model/domain.entity.builder.model";
import { DomainEntity } from "@app/shared/domain/model/domain.entity.model";

export class CashflowModel extends DomainEntity {
    value!: number;
}

export namespace CashflowModel {
	export class CashflowModelBuilder extends DomainEntityBuilder<CashflowModel> {
		public static Create() {
			return new CashflowModelBuilder(new CashflowModel());
		}

		public static CreateEmpty(): CashflowModel {
            return new CashflowModelBuilder(new CashflowModel())
                .withValue(0)
                .Make();
        }
        
		public withValue(value: number): CashflowModelBuilder {
			this.model!.value = value;
			return this;
        }
    }
}
