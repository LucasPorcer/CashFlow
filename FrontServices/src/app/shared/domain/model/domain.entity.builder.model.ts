import { DomainEntity } from './domain.entity.model';

export class DomainEntityBuilder<T> {

	protected model: T;

	protected constructor (model: T) {
		this.model = model;
	}

	public Make(): T {
		const instance = this.model;
		this.model = {} as T;
		return instance;
	}
}